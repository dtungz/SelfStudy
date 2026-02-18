# Lời mở đầu
> *"Good OO designs are reusable, extensible, and maintainable."*  
> — Head First Design Patterns

- Có thể tái sử dụng
- Có thể ở rộng
- Có thể duy trì

Ở Phase 1, bạn đã **trải nghiệm** những principles này mà không biết tên:

| Phase 1 Task | Bạn đã làm gì? | Phase 2 Principle sẽ học |
|--------------|----------------|--------------------------|
| **Task_Car** | Tách `speed`, `fuel` thành private fields | → Encapsulate What Changes |
| **Task_Weapon** | Car HAS Weapon (composition) | → Composition Over Inheritance |
| **Task_WeaponTypes** | Car dùng `IWeapon` interface | → Program to Abstraction |
| **Task_Events** | Dùng events để notify | → Low Coupling / High Cohesion |

Phase 2 **đặt tên chính thức** cho những gì bạn đã tự khám phá!
___


**SOLID là gì?**
SOLID là 5 nguyên tắc thiết kế OOP (từ Robert C. Martin - "Uncle Bob"):

| Chữ   | Tên đầy đủ            | Ý nghĩa ngắn gọn                                 | Ví dụ vi phạm                                                    |
| ----- | --------------------- | ------------------------------------------------ | ---------------------------------------------------------------- |
| **S** | Single Responsibility | Mỗi class chỉ làm 1 việc                         | `PlayerController` vừa xử lý input, vừa render UI, vừa save game |
| **O** | Open/Closed           | Mở để mở rộng, đóng để sửa đổi                   | Thêm enemy type → phải sửa `switch` trong code cũ                |
| **L** | Liskov Substitution   | Class con thay thế được class cha                | `Square` extends `Rectangle` nhưng `setWidth()` hoạt động sai    |
| **I** | Interface Segregation | Chia nhỏ interface                               | `IPlayer` có `Fly()` nhưng `GroundPlayer` không bay được         |
| **D** | Dependency Inversion  | Phụ thuộc abstraction, không phải implementation | `GameManager` gọi trực tiếp `PlayerPrefs.Save()`                 |

---
Tại sao không dạy SOLID trực tiếp?

SOLID giống như **5 điều luật** — nhưng luật không giải thích **tại sao** phải tuân theo.

Phase này dạy **4 mindsets** — là bản chất đằng sau SOLID:

| Mindset của Phase 2 | Bao gồm SOLID nào | Dẫn đến Pattern nào |
|---------------------|-------------------|---------------------|
| Encapsulate What Changes | O (Open/Closed) | **Strategy** |
| Composition Over Inheritance | L, I (Liskov, ISP) | **Decorator**, **Component** |
| Program to Abstraction | D (Dependency Inversion) | **Factory**, **Abstract Factory** |
| Low Coupling / High Cohesion | S (Single Responsibility) | **Observer**, **Command** |

Nếu hiểu 4 mindsets này, SOLID sẽ tự nhiên mà đến, và Design Patterns cũng vậy!
___
Phân biệt: Dependency Inversion vs Dependency Injection

Hai khái niệm này HAY BỊ NHẦM:

| Khái niệm | Là gì | Ví dụ |
|-----------|-------|-------|
| **Dependency Inversion (DIP)** | NGUYÊN TẮC — Depend on abstraction, not implementation | Dùng `ISaveService` thay vì `PlayerPrefs` |
| **Dependency Injection (DI)** | KỸ THUẬT — Cách truyền dependency vào | Inject qua constructor: `new GameManager(saveService)` |

```csharp
// Dependency Inversion: phụ thuộc interface
public class GameManager
{
    private ISaveService saveService;  // ← DIP: depend on abstraction
    
    // Dependency Injection: được truyền vào từ bên ngoài
    public GameManager(ISaveService saveService)  // ← DI: inject dependency
    {
        this.saveService = saveService;
    }
}
```

**DIP** = quy tắc cần theo  
**DI** = cách thực hiện DIP

---
Sau phase này, bạn sẽ:
- Biết **tách phần dễ thay đổi** ra
- Ưu tiên **Composition** hơn Inheritance
- Code theo **abstraction**, không phải implementation
- Thiết kế **low coupling, high cohesion**
___
# Principle 1: Encapsulate What Changes

> *"Identify the aspects of your application that vary and separate them from what stays the same."*  
> "*Hãy xác định những phần trong ứng dụng của bạn sẽ thay đổi, và tách chúng ra khỏi những phần không thay đổi.*"
> — Head First Design Patterns

- Trong phần này mình quay lại với interface, theo SOLID thì đây là chữ O (Open/Close)
- Bài này học về tính Encapsulate/ Tính đóng gói thì phải.
- Thể hiện ở việc mở để thêm và đóng để sửa.... hmm đi vào VD sẽ dễ hiểu hơn
	  - Before: mình sẽ code hết trong Player Movement -> Sửa 1 chỗ = sửa nhiều chỗ, fix đi fix lại trong 1 script.
	  - After: Mình sẽ dùng 1 interface là IMovement và cho 1 hàm Move(), ngoài ra thêm type để phân biệt nữa. Thì lúc này trong Player Movement thì gọi 1 cái IMovement curMove và trong update chạy hàm Move là được. Đối với Các hàm kế thừa interface thì mình code logic di chuyển vào hàm Move
	-> Từ đây, để mở rộng thì mình chỉ việc thêm các interface thôi. Và sửa chưa cx sẽ không ảnh hưởng gì đến thằng gọi là Player Movement