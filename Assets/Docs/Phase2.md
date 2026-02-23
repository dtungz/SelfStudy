# Ghi chú
- Từ phase 2 trở đi, mình sẽ copy các kiến thức từ bài giảng ròi sẽ thêm những kiến thức mình học được bên dưới mỗi phần
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
# Principle 2: Composition Over Inheritance
- Tại nguyên tắc này, ta sẽ đi sâu hơn về việc kết hợp nhiều behavior (các hành vi) tốt hơn về việc kế thừa
## Recall Phase 1 🔙

Bạn đã thấy principle này:
- **Task_Weapon**: Survivor HAS Weapon (không IS Weapon)
- **Task_WeaponTypes**: Pistol, Shotgun implement IWeapon (không kế thừa từ Weapon base)
- **SimUDuck story**: Vịt cao su bay được vì dùng inheritance!

---

## Feature: Ability System

Character cần có nhiều abilities:
- Attack (melee/ranged)
- Heal
- Shield
- Dash

Một character có thể có nhiều abilities khác nhau.

---

## Phần 1: Cách sai — Deep Inheritance

```
Character
├── MeleeCharacter
│   ├── MeleeWithHeal
│   └── MeleeWithShield
├── RangedCharacter
│   ├── RangedWithHeal
│   └── RangedWithDash
└── HybridCharacter  ← Melee + Ranged???
```

### Vấn đề

| Issue | Problem |
|-------|---------|
| Explosion of classes | 2 attacks × 4 abilities = 8+ classes |
| Diamond problem | MeleeWithHealAndShield? |
| Runtime lock | Không thể thay đổi abilities |
| Duplicate code | Heal logic ở nhiều nhánh |

Đây chính là vấn đề của **SimUDuck**!

---
### Ghi chú
- Phần 1 đã đưa ra vấn đề về việc kế thừa sâu bằng cách lấy ví dụ về một features là Ability System
- Các vấn đề được đưa ra : 
	- (1) Explosion of classes : Quá nhiều class -> Ví dụ ở đây ta thấy tại Melee thì có Heal và Shield, ở Range lại có Heal và Dash. Điều này khiến code bị phình to và gây ra các vấn đề dưới đây
	- (2) Diamond Problem : (không biết dịch....) -> Khi mà mình kế thừ sâu như MeleeWithHeal rồi xong mình lại có MeleeWithShield. Vấn đề đặt ra : 
		- Oke, vậy nếu như mà mình cần 1 thằng có nhiều ability như MeleeWithShieldAndHeal thì sao? 
		- Chả lẽ chúng ta lại kế thừa 1 thằng từ Melee và copy nguyên 2 phần của 2 con kia, hay tạo 1 con mới kế thừa 1 trong 2 con kia và thêm tính năng của con còn lại trong khi các ability là ngang nhau?
	- (3) Runtime Lock : Không thể thay đổi abilities -> YES vì từ cái tên đã nói rồi, MeleeHeal chả lẽ thêm Shield vào
	- (4) DuplicateCode : Heal logic ở nhiều nhánh -> Kiểu như mình tách cả MeleeHeal và RangeHeal ý, 2 thằng này đều có logic heal nên mình sẽ phải copy logic của thằng này sang cho thằng kia
		- Vấn đề to lớn : Khi mà mình muốn thay đổi logic heal, thì đó là ác mộng
- Tìm hiểu : về vấn đề thứ (2), thì đây là vấn đề về việc không xác định rõ quan hệ IS-A và HAS-A
        Character
        /       \
   Melee      Healer
        \       /
    MeleeWithHeal
    -> Cấu trúc hình kim cương

**CHAT GPT** : 
```

Khi cả `Melee` và `Healer` đều kế thừa `Character`, thì `MeleeWithHeal` sẽ có **2 bản sao Character**.

Trong C# thì không có multiple inheritance class → nhưng vấn đề logic vẫn tồn tại dưới dạng:

- Không biết nên kế thừa từ đâu
    
- Ability ngang hàng nhau nhưng bị ép phân cấp
    

Bạn đặt câu hỏi rất đúng:

> Nếu ability ngang nhau thì kế thừa ai?

👉 Đây chính là dấu hiệu bạn đang **misusing inheritance for feature composition**.

Inheritance nên dùng cho:

> IS-A relationship (phân loại bản chất)

Không nên dùng cho:

> HAS-A capability variation
```
## Phần 2: Giải pháp — Composition

### Interface cho ability

```csharp
public interface IAbility
{
    string Name { get; }
    float Cooldown { get; }
    bool IsReady { get; }
    
    void Activate();
}
```

### Các abilities

```csharp
public class MeleeAttack : IAbility
{
    public string Name => "Melee Attack";
    public float Cooldown => 1f;
    public bool IsReady => Time.time >= nextUseTime;
    
    private float nextUseTime;
    
    public void Activate()
    {
        if (!IsReady) return;
        
        // Melee attack logic
        nextUseTime = Time.time + Cooldown;
    }
}

public class HealAbility : IAbility
{
    public string Name => "Heal";
    public float Cooldown => 5f;
    public bool IsReady => Time.time >= nextUseTime;
    
    private float nextUseTime;
    private int healAmount = 20;
    
    public void Activate()
    {
        if (!IsReady) return;
        
        // Heal logic
        nextUseTime = Time.time + Cooldown;
    }
}
```

### Character với Composition

```csharp
public class Character : MonoBehaviour
{
    private List<IAbility> abilities = new List<IAbility>();
    
    public void AddAbility(IAbility ability)
    {
        abilities.Add(ability);
    }
    
    public void RemoveAbility(IAbility ability)
    {
        abilities.Remove(ability);
    }
    
    public void UseAbility(int index)
    {
        if (index < abilities.Count)
        {
            abilities[index].Activate();
        }
    }
}
```

---
### Ghi chú
- Đây là hướng giải quyết với interface
- Trong game sẽ thay string = enum
- Tại đây mình nghĩ là Melee và Range nên tách thành 1 cái vì nó liên quan đến cách đánh
- Còn các cái như Hield, Shield .. thì là 1 cái do nó liên quan đến kĩ năng
	- Nói chúng đấy là cách thiết kế game của mỗi người muhehehehe

## Phần 3: Unity Component Style

Từ **Game Programming Patterns**:

> *"The Component pattern allows a single entity to span multiple domains without coupling the domains to each other."*

Unity đã dùng Composition sẵn!

```csharp
// Thay vì inheritance
public class MeleeCharacter : Character { }

// Dùng components
public class Character : MonoBehaviour
{
    // Có sẵn các components
}

// Thêm ability như component
public class MeleeAttackComponent : MonoBehaviour, IAbility
{
    public void Activate() { /* ... */ }
}
```

### Setup trong Unity

```csharp
// Build character bằng components
GameObject warrior = new GameObject("Warrior");
warrior.AddComponent<Character>();
warrior.AddComponent<MeleeAttackComponent>();
warrior.AddComponent<ShieldComponent>();
```

---

## 🎉 Pattern Previews

### Decorator Pattern
> *"Attach additional responsibilities to an object dynamically."*

Composition cho phép "wrap" thêm behaviors!

### Component Pattern
Từ Game Programming Patterns — decouples domains trong game objects.

> [!TIP]
> Ở Phase 3, bạn sẽ học **Decorator Pattern** chính thức!

---
### Ghi chú
**Không biết nói gì về phần này :,<**

## Phần 4: Thực hành

### Bước 1: Tạo `IAbility` interface

### Bước 2: Tạo 3 abilities
- `MeleeAttack`
- `RangedAttack`
- `HealAbility`

### Bước 3: Tạo `Character` class
- Có `List<IAbility>`
- Methods: `AddAbility`, `UseAbility`

### Bước 4: Test combinations
- Warrior: Melee + Heal
- Mage: Ranged + Heal
- Paladin: Melee + Ranged + Heal

---

### Kiểm tra

- ✅ Không có inheritance hierarchy
- ✅ Character có thể có bất kỳ combination nào
- ✅ Thêm ability mới không sửa Character
- ✅ Có thể add/remove abilities runtime

---

### Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Composition** | Character HAS abilities |
| **vs Inheritance** | Character IS NOT a type |
| **Flexibility** | Mix and match |
| **Runtime changes** | Add/remove behaviors |
| **Component Pattern** | Unity's design philosophy |

---

### So sánh

| Inheritance | Composition |
|-------------|-------------|
| IS-A relationship | HAS-A relationship |
| Compile-time | Runtime flexibility |
| Single inheritance (C#) | Multiple behaviors |
| Rigid | Flexible |
| Share implementation | Share contract (interface) |

---

### Khi nào dùng Inheritance?

Inheritance vẫn hữu ích khi:
- Có quan hệ **IS-A rõ ràng** (Dog IS-A Animal)
- Muốn **share implementation** (không chỉ interface)
- Hierarchy **ổn định, ít thay đổi**
- **Max 1-2 levels** deep

---
### Ghi chú
- Yeah việc làm phần 4 này mất kha khá time do bận khá nhiều việc linh tinh hehe. Nhưng dù sao thì mình cũng đã hoàn thành xong cái này. Nhưng mà vẫn chưa test được cái phần 4 bước cuối.... Dù sao thì phần save game mình cũng sẽ xem lại kĩ hơn :>