# Phase 1
## Module 1
### Task Survivor
- Tính đóng gói : Theo cách hiểu tự nhiên, đóng gói tức là 1 chiếc hộp có 2 ngăn
	-  Ngăn thuộc tính (Properties) : Là các thống số, các chỉ số hay hiểu đơn giản là các bộ phận của một con robot (VD : Tay, chân, đầu, tốc độ di chuyển....)
	- Ngăn hành động (Methods) : Là các hành động của con robot đó (VD : Đi bộ, chạy nhảy)
- Những giá trị của thuộc tính được thay đổi được nên được bảo vệ bằng private, để lấy hoặc thay đổi thì nên có những hàm get set riêng để có thể lấy được các giá trị của các thuộc tính đó
> [!TIP]
> Câu hỏi *"What varies?"* là nền tảng của nguyên tắc *"Encapsulate what varies"*!


#### Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| Class | `Survivor` là model của nhân vật |
| Encapsulation | `private` fields + `public` methods |
| Validation | Kiểm tra logic trong methods |
| **"What varies?"** | `currentHealth`, `moveSpeed` hay thay đổi → private |

---

### Task Player
#### Mục tiêu 
- Hiểu **Separation of concern** - Mỗi class có một trách nhiệm riêng

	-  Mỗi một class chỉ nên có 1 trách nhiệm riêng biệt (VD : PlayerInput thì chỉ nhận input của người chơi, HealthComponent chỉ quản lý máu của 1 object)
	- Các class chỉ làm nhiệm vụ của mình, không nên biết quá sâu về các class khác (VD : PlayerInput thì không nên biết cách hoạt động của HealthComponent, chỉ cần biết là bản thân nó sẽ gửi thông tin input của người chơi cho Survivor và những việc còn lại thì sẽ là của Survivor như là di chuyển trái phải,...)
		- Điều này giúp việc khi thay đổi chỉ cần thay đổi ít (VD : khi lỗi input thì chỉ cần sửa trong input, không liên quan đến Survivor movement. Hay khi nhân vật di chuyển bị lỗi thì chỉ cần sửa trong survivor movement và không liên quan gì đến input hay health)
		
| Class    | Trách nhiệm                     | KHÔNG lo                         |
| -------- | ------------------------------- | -------------------------------- |
| Player   | Đọc input, ra lệnh di chuyển    | Cách Survivor implement movement |
| Survivor | Quản lý health, speed, position | Input đến từ đâu                 |
> *"Each class should have only one reason to change."*

#### Quy tắc HAS-A / IS-A
- Một object nên phân biệt rõ has-a (có cái gì) và is-a (là cái gì)
	-  Survivor **HAS-A** Weapon
	-  Piston **IS-A** Weapon
	-  Player **HAS-A** Survivor chứ không phải Survivor **IS-A** Player
> [!TIP]
> *"HAS-A can be better than IS-A"* — Head First Design Patterns
> 
> Đây là preview cho nguyên tắc **"Favor Composition over Inheritance"**!

#### Kiến thức rút ra

| Khái niệm        | Áp dụng                                   |
| ---------------- | ----------------------------------------- |
| Encapsulation    | Player không biết internal của Survivor   |
| Public interface | Player chỉ dùng public methods            |
| Separation       | Player = input, Survivor = logic nhân vật |
| **HAS-A**        | Player "có" Survivor, không "là" Survivor |

---
### Task Weapon
#### Mục tiêu
Hiểu **Composition** — "HAS-A" relationship.

> *"Favor composition over inheritance."* — Head First Design Patterns

#### Kiến thức
- Compisition trong Unity?
	- Composition theo cách hiểu của tôi là việc chia các công việc thành các script nhỏ, kiểu quản lý
	- VD như việc Enemy sở hữu máu, enemy có thể di chuyển thì có thể tách ra 2 script là HealthComponent và Enemy Movement. Trong đó HeathComponent có thể dùng cho cả Survivor
- Không sử dụng kế thừa linh tinh
❌ `Survivor : Weapon`? → Nhân vật không phải là vũ khí!  
❌ `Weapon : Survivor`? → Vũ khí không phải là nhân vật!  
✅ `Survivor` **có** `Weapon` → Hợp lý!
		-Tức là mình sẽ không sử dụng kế thừa với **HAS-A**, thay vào đó nên serialize kéo thả hoặc get component
#### Kiến thức rút ra
- Composition : Survivor "có" Weapon -> Hiểu theo cách trong unity thì tức là mình sẽ có 2 script là survivor và weapon là 2 component, sau đó trong survivor thì mình sẽ tạo 1 biến weapon (survivor CÓ weapon)
- Auto-fire Parttern : weapon tự hoạt động độc lập -> tại đây thì sẽ hiểu như là việc mỗi script sẽ làm việc của nó, kiểu như thằng player hay thằng survivor sẽ chả cần biết khổ súng hoạt động như nào, việc auto fire là việc của khổ súng, do weapon quản lý
- State Management : Weapon tự động quản lý cooldown, damage -> tương tự cái trên, việc cooldown hay damage của khổ súng thì đấy là việc của khổ súng, survivor không cần biết về điều đó
- HAS-A > IS-A : Composition thay vì inheritance -> ở đây hiểu như là việc kế thừa sẽ bó buộc cái đó phải làm theo cha của chúng thay vì composition. Composition được me(tôi, em, tớ, ta) hiểu như là một vật thể có thể custom, thêm tính năng hoặc loại bỏ tính năng một cách tự do. Ví dụ như việc EnemyWalk thì mình kế thừa EnemyBase có di chuyển và đấm nhau. Nhưng về sau tôi muốn để ra 1 enemy không thể di chuyển hoặc enemy không thể đấm nhau thì chả lẽ lại không kế thừa, hay kế thừa xong overrite lại một hàm không viết gì cả, thay vào đó nên là enemy nên có các composition như move hay attack component 
### Task Zombie
#### Mục tiêu
- Tạo Zombie với AI đơn giản
- **Nhận ra vấn đề coupling** (sẽ fix ở Module 3)
#### Vấn đề của task
- Tại đề bài đang dùng FindObjectType
- Thì việc dùng vậy sẽ tạo ra khá nhiều tình huống không nên có 

| Tình huống | Kết quả |
|------------|---------|
| Có nhiều Survivors? | Lấy survivor nào? |
| Survivor chưa spawn? | **NullReferenceException!** |
| Muốn test Zombie riêng? | Phải có Survivor trong scene |
| Đổi tên class Survivor? | Phải sửa tất cả Zombie |
Theo như bài thì đây là Tigh Coupling thì Zombie đang phụ thuộc rất nhiều vào Survivor, phải có survivor mới có thể hoạt động

> *"Strive for loosely coupled designs between objects that interact."*  
> — Head First Design Patterns

#### Ghi nhớ câu hỏi này

> **Làm sao để Zombie không cần biết về Survivor class cụ thể?**

Đây là vấn đề bạn sẽ giải quyết ở [Module 3: Dependency](../Module3_Dependency/README.md):
- **Dependency Injection (đơn giản)**: Ai đó "đưa" target vào, không tự tìm
- **Interface Abstraction**: Zombie chỉ cần `ITarget`, không cần biết là Survivor
(Copy từ bài giảng, đã suy nghĩ nhưng chưa ra hướng giải quyết, sẽ sửa thêm trong các commit tiếp theo)

#### Kiến thức rút ra
- Các object không nên quá phụ thuộc vào nhau, chúng chỉ nên tương tác với nhau
- FindObjectType là **ĐẦN**
### Hoàn thành Module 1
| Task     | Concept                                  |
| -------- | ---------------------------------------- |
| Survivor | Encapsulation, "What varies?"            |
| Player   | Separation of Concerns, Input handling   |
| Weapon   | Composition, HAS-A relationship          |
| Zombie   | Object interaction, **Coupling problem** |

---
