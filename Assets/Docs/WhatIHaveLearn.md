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
- HAS-A > IS-A : Composition thay vì inheritance -> ở đây hiểu như là việc kế thừa sẽ bó buộc cái đó phải làm theo cha của chúng thay vì composition. Composition được me(tôi, em, tớ, ta) hiểu như là một vật thể có thể custom, thêm tính năng hoặc loại bỏ tính năng một cách tự do. Ví dụ như việc EnemyWalk thì mình kế thừa EnemyBase có di chuyển và đấm nhau. Nhưng về sau tôi muốn để ra 1 enemy không thể di chuyển hoặc enemy không thể đấm nhau thì chả lẽ lại không kế thừa, hay kế thừa xong override lại một hàm không viết gì cả, thay vào đó nên là enemy nên có các composition như move hay attack component
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
Refactor : Mỗi giây check 8~10 lần
## Module 2
- Đi sâu vào HAS-A ở module 1
### Task Zombie
#### Mục tiêu
- Học **khi nào** Inheritance là lựa chọn đúng
- Hiểu **abstract class** và **polymorphism**
> [!NOTE]
> **OCP = Open/Closed Principle** — Class nên mở cho extension, đóng cho modification.  
> Bạn sẽ học chi tiết ở Phase 2!

Bài học rút ra :
- Inheritance dùng khi nào : Dùng khi quan hệ IS-A được xác định rõ ràng -> Ví dụ trong game của mình thì IS-A được xác định như kiểu Fast hay Tank hay Basic Enemy là Enemy ý, thì khi đấy các thành phần kia sẽ là Enemy và sẽ kế thừa những hàm chắc chắn có như là di chuyển, tấn công, công trừ máu, các Protities, các ref bla bla. **NHƯNG** những hàm có thể mở rộng như Attack hay Move thì mình sẽ có thêm những hàm mở rộng như MoveLogic(){} thì trong đó mình sẽ gọi hàm OnMove thì hàm OnMove mình sẽ override nó để có thể mở rộng (extension maybe)
- Các subclasses **share significant behavior** (di chuyển về phía target, chết khi HP = 0)
    - Yeh thì đây là những gì mình đã nói ở trên, như là những hàm di chuyển các thứ, như trong game của mình thì enemy sẽ target vào sur rồi quay đầu về phía đó, xong đi thẳng về phía quay đầu. Đối với mình thì logic của mọi enemy đều như vậy, điều khác chắc có lẽ là khoảng cách nhưng về logic thì vẫn thế
- Hierarchy **không quá sâu** (1-2 levels) : cái này mình không hiểu rõ lắm, nhưng theo mình tìm hiểu (chat GPT) thì đây sẽ là kiểu 1 class sẽ không kế thừa quá sâu, kiểu như thg thằng tank sẽ kế thừa enemy thôi thì đây là 1 levels, còn nhiều levels là kiểu hmmm.... maybe đứa con -> child -> people -> social -> earth kiểu kiểu đó

- Vậy Inheritance không dùng khi nào???
    - Chỉ muốn tái sử dụng code : theo nguồn chat gpt thì chỉ muốn tái sử dụng code sẽ là kiểu không nên kế thừa chỉ để khỏi copy. Kiểu như là việc mình muốn sử dụng Sound thì mình sẽ có hàm play Sound, thì mình không nên để cái cửa, player, chest, etc... kế thừa Sound chỉ để gọi hàm playsound
    - Objects cần **mix behaviors** (như vịt vừa bay vừa kêu) : cái này mình hiểu như là việc một thằng enemy có khả năng dịch chuyển thì mình không nên ném vào thằng base enemy vì có thể nhiều thằng enemy sẽ không cần dịch chuyển các thứ, thay vào đó thì mình nên làm nó thành 1 interface hay composition chẳng hạn hoặc code riêng nếu chỉ nó có teleport. **TỨC LÀ KIỂU** có những thứ mà objecct này có nhưng object kia không có. Mình nghĩ chỉ nên kế thừa khi mà tất cả các object cùng kế thừa có ít nhất là tất cả đặc điểm mà cái base có.
    - Hierarchy phức tạp (3+ levels) : Tất nhiên, như mình đã nói ở trên thì kế thừa với 3+ lv thì việc bị bug khá dễ, và khi kiếm thằng cha bị bug trong đống kế thừa thì có thể là die

| Khái niệm            | Áp dụng                                  |
| -------------------- | ---------------------------------------- |
| **Abstract class**   | `ZombieBase` định nghĩa template         |
| **Inheritance**      | Các loại zombie kế thừa behavior chung   |
| **Polymorphism**     | `Attack()` làm khác nhau tùy loại        |
| **Virtual/Override** | Customize behavior khi cần               |
| **When to inherit**  | IS-A, shared behavior, shallow hierarchy |
- Abtract class : là thằng gốc, chứa tất cả những gì mà thằng con cần có, tại đây thì mình sẽ định nghĩa các hàm mà cần dùng (i dunnu what i saying because i writing this at 1am....). Kiểu kiểu như là một chiếc toyota thì sẽ kế thừa từ 1 thằng o tô, và trong thằng ô tô sẽ định nghĩa kiểu Move(), hay QuayXe().... maybe cả Fly() nếu như tương lai tất cả ô tô đều có thể bay.
- Inheritance : đây sẽ là thằng con, kế thừa tất cả những gì thằng cha nó có và phát triển lên, ví dụ như thằng toyota bên trên thì nó sẽ có các hàm move()...
    - Một số ghi chú nếu trong tương lai mình quên : trong thằng cha nếu mình để hàm là:
        - private : thằng con không truy cập được, nó vẫn tồn tại, vẫn dùng được trong hàm của thằng cha nhưng thằng con không thể truy cập (1) -> mình sẽ dùng ví dụ cho mình của tương lai dễ hiểu bên dưới
        - protected : thằng con có thể gọi lại cái này, ví dụ protected void Attack() thì thằng con có thể gọi được
        - protected virtual .... : thằng con có thể viết lại hàm đó bằng override, nếu vẫn muốn dùng cái cũ thì gọi base."tên hàm"
        - public : hết cứu
    - VÍ dụ : class cha
  ```C#
  public class Enemy
    {
    private int hp = 100;                // (1) private
    protected int damage = 10;           // (2) protected

    public void TakeDamage(int amount)   // (3) public
    {
        hp -= amount;
        DieCheck();
    }

    protected virtual void Attack()      // (4) protected virtual
    {
        Console.WriteLine("Enemy attack: " + damage);
    }

    private void DieCheck()
    {
        if (hp <= 0)
            Console.WriteLine("Enemy dead");
    }

	```
	- Ví dụ : class con
	``` C#
	public class MageEnemy : Enemy
    {
    public void Cast()
    {
    // hp = 50;        ❌ lỗi vì hp là private
    damage = 20;       // ✔ dùng được vì protected
    Attack();          // ✔ gọi được vì protected
    }

    protected override void Attack()
    {
        base.Attack();     // gọi bản gốc (optional)
        Console.WriteLine("Mage casts fireball!");
    }


- Polymorphism : `Attack()` làm khác nhau tùy loại : Tức là cái virtual ý
- Virtual với override : Theo ý hiểu thì là custom lại hàm chăng...
- When to inherit :
    - Có quan hệ IS-A thực sự
        - Đặt câu hỏi : class con có phải là 1 dạng đặc biệt của class cha không, cái ... có phải là .. không
            - Tank is Enemy
            - Piston is Weapon
            - Survivor **IS NOT** Player : player just controller survivor
    - Có chung hành vi
        - Tank có chung hành vi của enemy
        - Có máu
        - có target
        - Có thể tấn công
        - Có thể di chuyển
    - Cấu trúc kế thừa không sâu
        - Bug 1 thằng ở giữa là hết cứ....
### Task Weapon
#### Mục tiêu

  - Học **Interface** và tại sao nó flexible hơn inheritance
  - Hiểu nguyên tắc **"Program to Interfaces"**
  - **Bonus**: Đây chính là **Strategy Pattern**! 🎉

#### Yêu cầu : Trong survival shooters, mỗi weapon có đặc điểm riêng:
  - [x] **Pistol**: Đơn phát, damage trung bình, fire rate cao
  - [x] **Shotgun**: Nhiều đạn mỗi lần bắn, damage cao ở gần, spread rộng
  - [x] **Laser**: Xuyên qua nhiều targets, damage thấp, continuous beam

#### Mình đã học được gì???

> *"Program to an interface, not an implementation."*  
> — Head First Design Patterns

- Theo mình hiểu ở đây, thì nên sử dụng các interface hoặc abtracs thay vì phụ thuộc hẳn vào 1 class cụ thể
  - Ví dụ thay vì sử dụng WP_Piston thì mình có thể sử dụng IWeapon được vì khi Piston đã interface tới IWeapon thì chúng ta hoàn toàn có thể IWeapon newGun = new WP_Piston() kiểu kiểu đó
  - Tức là ở đây mình có thể flexible với các cái khác mà không phụ thuộc hẳn vào. Kiểu như nếu mình đã ép chặt nó vào Piston thì không thể chuyển qua ShotGun hay Laser được nhưng khi sử dụng với Weapon thì có thể flex sang các vụ khí khác
  - Theo nguồn đáng tin cậy để diễn đạt ý hiểu của mình thì việc này giúp giảm phụ thuộc, dễ mở rộng code,...

| Benefit | Giải thích |
|---------|------------|
| Survivor không biết đang dùng Pistol, Shotgun hay Laser | **Loose coupling** |
| Có thể đổi weapon runtime (pickup!) | **Flexibility** |
| Dễ thêm loại weapon mới | **Open for extension** |
| Dễ test | Mock IWeapon |

- Như bảng trên thì từng benefit mình sẽ đưa ra với so với code của mình nhóe :>
  - (1) Trong code tại script Weapon, mình đặt currentWeapon là 1 IWeapon, tức là không phải 1 Piston, Shotgun hay Laser, chỉ là 1 vũ khí, giống như là thằng sur chỉ biết là nó đang cầm 1 cái vụ khí chứ chả biết mình đang cầm vũ khí gì. Từ đó giống như là mình nói là nó đang cầm vũ khí gì thì nó đang cầm vụ khí đó vậy
  - (2) Trong runtime tức là khi đang play thì mình có thể đổi vũ khí qua hàm ChangeWeapon, kiểu như là mình đang bảo thg sur là mày đang cầm súng lúc, khi mà mình muốn đổi vũ khí thì mình gọi change weapon, giống như là lúc sau mình bảo thg sur là mày đang cấm shotgun vậy, điều này có thể hoạt động là nhờ mình không cho nó biết nó đang cầm cái gì,  chỉ là 1 cái IWeapon để mình có thể linh hoạt chuyển nó sang các vụ khí đang interface  với cái IWeapon
  - (3) Yeh, việc thêm weapon mới thì ta chỉ việc code thêm 1 thg vũ khí mới và nó phải interface với cái IWeapon là được, miễn là đăng kí thì mình có thể sử dụng nó như là 1 vũ khí, kiểu ta đăng kí cái dép là 1 iweapon thì cái thg sur cx có thể cầm dép làm weapon
  - (4) hmmm, i dun understand this... theo như mình hiểu (theo chatgpt) thì việc dùng interface dễ dàng ở việc khi mình làm test thì mình không cần phải code hẳn ra 1 hàm.... kiểu như là mình muốn test nó có hoạt động không thì thay vì mình phải tạo hẳn ra 1 hàm WP_Piston thì mình chỉ cần tạo ra 1 script kiểu MockWeapon xong kiểu trong Attack mình code ra cái FireCount++ gì đó xong Debug ra để biết có hoạt động không (maybe i think so)
- (4) : i think i need đặt example ở here
  - Ví dụ ta có 1 class quản lý

```C#
public class WeaponController
{
    private IWeapon _currentWeapon;

    public void Equip(IWeapon weapon)
    {
        _currentWeapon = weapon;
    }

    public void Update()
    {
        if (_currentWeapon != null && _currentWeapon.CanAttack)
        {
            _currentWeapon.Attack();
        }
    }
}


```
Nếu không dùng interface ta sẽ phải

`private WP_Piston _weapon`

- Mình sẽ bắt buộc phải tạo WP_Piston thật
- Phải có monobehavior
- Phải có scena
- Phải có cooldown thật
-> Khó test tự động

- Nếu dùng IWeapon thì ta có thể tự tạo mock

```C#
class MockWeapon : IWeapon
{
    public float Damage => 0;
    public float Cooldown => 0;
    public bool CanAttack { get; set; }
    public WeaponType Type => WeaponType.Pistol;

    public int AttackCallCount = 0;

    public void Attack()
    {
        AttackCallCount++;
    }
}

```

- Test Logic

```C#
var controller = new WeaponController();
var mock = new MockWeapon();

controller.Equip(mock);

mock.CanAttack = true;
controller.Update();

Debug.Assert(mock.AttackCallCount == 1);

```

- Nếu mình không nhầm thì cái này là 1 tool :L 
- And damn, cái này là chữ D trong solid =)) : Dependency Inversion Principle
  - Tức là module cấp cao không phụ thuộc vào class cụ thể (Chat gpt)
  - Kiểu thay vì thằng player là module cấp cao ý, nó sẽ không cầm piston mà nó cầm weapon =)) 


- Trong bài giảng có nói

> *"Define a family of algorithms, encapsulate each one, and make them interchangeable."*  
> — Head First Design Patterns

Bạn vừa implement **Strategy Pattern** mà không biết!

| Strategy Pattern | Weapon Example |
|-----------------|----------------|
| Context | Survivor |
| Strategy Interface | IWeapon |
| Concrete Strategies | Pistol, Shotgun, Laser |

> [!TIP]
> Ở Phase 3, bạn sẽ học chính thức về Strategy Pattern. Bây giờ, bạn đã hiểu nền tảng!


#### Kiến thức rút ra
| Khái niệm | Áp dụng |
|-----------|---------|
| **Interface** | `IWeapon` định nghĩa contract |
| **Composition** | Nhiều interfaces nhỏ thay vì 1 class lớn |
| **Program to interface** | Survivor dùng `IWeapon`, không dùng `Pistol` |
| **Flexibility** | Swap weapon dễ dàng |
| **Strategy Pattern** | Đây là foundation cho Pattern đầu tiên! |

- (1) Chỗ này mình hiểu như là interface là một bản hợp đồng, khi kí hợp đồng thì phải bắt buộc các điều khoản, và các điều khoản sẽ được khai báo trong interface như là OnMove(), float damage....
- (2) Composition thì ở những phần đầu mình đã nói là nhiều script thay vì 1 script. And yeh, bây giờ mình sẽ hiểu nó là chia nhỏ các tính năng ra. Kiểu thay vì ta có 1 tính năng là Ô tô, thì ta sẽ có các tính năng là Chạy, Hết xăng, Bay, Đèo người... và những cái đó là các composition của 1 ô tô??
- (3) "Hãy phụ thuộc vào interface thay vì phụ thuộc vào các class cụ thể"
- (4) Yeh như ví dụ thì IWeapon có thể = Piston, có thể = Shotgun...
- (5) Cái này học ở các phase sau heh

#### So sánh tổng kết
| Inheritance | Interface + Composition |
|-------------|------------------------|
| "is-a" relationship | "can-do" relationship |
| Rigid hierarchy | Flexible combinations |
| Share implementation | Share contract |
| One parent only | Multiple interfaces |
| Couples to base class | Loose coupling |

- (1)
  - IS-A : Tank là quái, Toyota là ô tô
  - Can-Do : Laser có khả năng gây damage.
    - IWeapon : Can Attack
    - IDamageable : Có thể nhận damage
    - IMoveable : Có thể di chuyển
- (2) : 1 người thì chỉ có một bố (Inheritance) => Cứng nhắc. Nhưng 1 người có thể kí nhiều bản hợp đồng (Interface + composition) => Linh hoạt
- (3) : 
  - Class con nhận toàn bộ từ class cha => mọi sub class bị ảnh hưởng
    - coupling mạnh (coupling là kiểu độ kết dính, độ phụ thuộc)
    - Nếu base sai thì tất cả các con đều sai
  - Interface không chia sẻ code, chỉ yêu cầu những thằng đăng kí hợp đồng bắt buộc phải có những điều khoản
    - Đã đăng kí IWeapon thì phải Attack()
- (4) : Như cái (2)
- (5) : Đối với kế thừa thì phụ thuộc rất nhiều vào base class, còn Interface thì không