# دليل المطور المتقدم 🎮

## 📚 شرح بنية المشروع

### PlayerController.cs
**الوظيفة:** إدارة تحكم اللاعب الكامل

```csharp
// المتغيرات الرئيسية:
- moveSpeed: سرعة الحركة (5)
- jumpForce: قوة القفز (5)
- punchDamage: ضرر الكم (10)
- maxHealth: الصحة العليا (100)

// الدوال الأساسية:
- HandleInput(): التعامل مع مدخلات لاعب
- Move(): حركة اللاعب
- Jump(): نظام القفز
- Punch(): نظام الكم مع اكتشاف الاصطدام
- TakeDamage(): نظام استقبال الضرر
```

### EnemyController.cs
**الوظيفة:** إدارة العدو والذكاء الاصطناعي

```csharp
// المتغيرات الرئيسية:
- detectionRange: نطاق الكشف (15)
- attackRange: نطاق الهجوم (2.5)
- moveSpeed: سرعة المطاردة (3.5)
- attackDamage: ضرر الهجوم (15)
- maxHealth: الصحة العليا (80)

// الدوال الأساسية:
- DetectPlayer(): كشف اللاعب
- ChasePlayer(): مطاردة اللاعب
- AttackPlayer(): هجوم اللاعب
- TakeDamage(): استقبال الضرر
```

### CharacterModel.cs
**الوظيفة:** بناء نماذج الشخصيات ثلاثية الأبعاد

```csharp
// الشخصيات المتاحة:
- Player Model: أزرق (رأس، جسم، أطراف، عيون)
- Enemy Model: أحمر (رأس، قرون، عيون حمراء، فم)

// الدالة الرئيسية:
- CreateCharacterModel(): بناء الشخصية
```

### UIManager.cs
**الوظيفة:** إدارة الواجهة وأشرطة الصحة

```csharp
// المتغيرات:
- playerHealthBar: شريط صحة اللاعب
- enemyHealthBar: شريط صحة العدو
- gameStatusText: نص حالة اللعبة

// الدوال:
- UpdateHealthBars(): تحديث الأشرطة
- CheckGameStatus(): فحص الفوز/الخسارة
```

---

## 🎮 كيفية تخصيص اللعبة

### تغيير سرعة اللاعب
```
Inspector → PlayerController → Move Speed
```
- **أقل من 5**: بطيء
- **5 (افتراضي)**: متوازن
- **أكثر من 5**: سريع جداً

### تغيير قوة القفز
```
Inspector → PlayerController → Jump Force
```

### تغيير ضرر الكم
```
Inspector → PlayerController → Punch Damage
```

### تغيير ذكاء العدو
```
Inspector → EnemyController → Detection Range
```
- زيادة النطاق = عدو أكثر عدوانية
- تقليل النطاق = عدو أقل نشاطاً

### تغيير سرعة المطاردة
```
Inspector → EnemyController → Move Speed
```

---

## 🔧 إضافة ميزات جديدة

### إضافة صوت للكم

```csharp
// في PlayerController.cs - في دالة Punch()
AudioSource.PlayClipAtPoint(punchSound, transform.position);
```

### إضافة رسوم متحركة

```csharp
// في PlayerController.cs
Animator animator;

private void Awake()
{
    animator = GetComponent<Animator>();
}

private void Move()
{
    // تشغيل حركة المشي
    animator.SetFloat("Speed", moveDirection.magnitude);
}
```

### إضافة عدو ثاني

```csharp
// في EnemyController.cs
[SerializeField] private bool isSecondaryEnemy = false;

private void Start()
{
    if (isSecondaryEnemy)
    {
        // قيم مختلفة للعدو الثاني
        moveSpeed = 2.5f;
        attackDamage = 8f;
    }
}
```

### إضافة باور أبس

```csharp
// ملف جديد: PowerUp.cs
public class PowerUp : MonoBehaviour
{
    public void IncreaseHealth(PlayerController player)
    {
        player.Heal(20f);
    }
    
    public void IncreaseDamage(PlayerController player)
    {
        player.IncreasePunchDamage(5f);
    }
}
```

---

## 🐛 حل المشاكل الشائعة

### اللاعب يسقط من الخريطة
**السبب:** قيمة Gravity كبيرة جداً أو الأرضية مفقودة
```
الحل: Project Settings → Physics → اضبط Gravity على -9.81
```

### العدو لا يرى اللاعب
**السبب:** Detection Range صغير جداً أو Layer غير صحيح
```
الحل: زيادة Detection Range أو التحقق من Layer المعين
```

### اللكم لا يعمل
**السبب:** Tag العدو ليس "Enemy"
```
الحل: انقر على العدو → Top Inspector → عين Tag = "Enemy"
```

### أشرطة الصحة لا تظهر
**السبب:** UIManager لم يتم ربطه بشكل صحيح
```
الحل: تأكد من ربط جميع المراجع في UIManager
```

---

## 📊 نظام النقاط المقترح

```csharp
public class GameManager : MonoBehaviour
{
    private int playerScore = 0;
    
    public void AddScore(int points)
    {
        playerScore += points;
        // تحديث الواجهة
    }
    
    public int GetScore()
    {
        return playerScore;
    }
}
```

---

## 🎯 تحسينات مستقبلية

1. **نظام مستويات:** زيادة صعوبة مع كل مستوى
2. **معدات:** خوذات، سلاح، درع
3. **ألعاب متعددة:** لعب ضد اللاعبين الآخرين
4. **بيئات متنوعة:** ساحات قتال مختلفة
5. **نظام مهارات:** مهارات خاصة للاعب والعدو

---

## 📝 ملاحظات تطويرية

- **Performance:** استخدم Object Pooling للعدو المتعدد
- **Quality:** استخدم LOD Levels للرسومات
- **Optimization:** قلل عدد الكائنات المرئية المعقدة
- **Testing:** اختبر مع أجهزة مختلفة

---

**حظ موفق في التطوير!** 🚀
