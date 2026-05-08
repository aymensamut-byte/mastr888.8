دليل إعداد لعبة القتال ثلاثية الأبعاد 🎮

# خطوات الإعداد الكاملة

## المرحلة الأولى: إعداد المشروع

### 1. فتح/إنشاء مشروع Unity
- استخدم **Unity 2020 LTS** أو أحدث
- اختر 3D Render Pipeline

### 2. استيراد الملفات
- ضع مجلد `Assets` في جذر المشروع
- يجب أن تجد المجلدات:
  ```
  Assets/Scripts/
  ├── Player/
  ├── Enemy/
  ├── Character/
  └── UI/
  ```

---

## المرحلة الثانية: إعداد المشهد

### 3. إنشاء المشهد الأساسي
```
Menu → File → New Scene
```

### 4. إضافة الإضاءة
1. **Directional Light** (للإضاءة الرئيسية)
   - Right Click → Light → Directional Light
   - اجعل الزاوية: (45, 45, 0)
   - Intensity: 1.2

2. **Ambient Light** (إضاءة محيطة)
   - Menu → Window → Rendering → Lighting Settings
   - اضبط Ambient Intensity على 0.5

### 5. إنشاء الأرضية
1. Right Click → 3D Object → Plane
2. اسم: `Ground`
3. Scale: (10, 1, 10)
4. Position: (0, -2, 0)
5. أضف Material أخضر (أو أي لون تفضله)

### 6. إعداد الفيزياء
```
Menu → Edit → Project Settings → Physics
```
- Gravity: (0, -9.81, 0) ✓ (الافتراضي)

---

## المرحلة الثالثة: إعداد اللاعب

### 7. إنشاء كائن اللاعب
```
Right Click → 3D Object → Cube
الاسم: Player
```

### 8. تكوين مكونات اللاعب
```
1. Transform:
   - Position: (0, 1, 0)
   - Scale: (0.6, 1, 0.4)

2. Rigidbody:
   - Mass: 1
   - Drag: 0
   - Angular Drag: 0.05
   - Freeze Rotation: (X, Y, Z) ✓ All
   - Collision Detection: Continuous

3. Box Collider:
   - Size: (0.6, 1, 0.4)
   - Is Trigger: ✗ (غير مفعل)

4. Layer: "Player"
```

### 9. إضافة Scripts للاعب
1. أضف Component: `PlayerController.cs`
2. أضف Component: `CharacterModel.cs`
   - عين `isPlayer = true`

### 10. تكوين المراجع في PlayerController
في Inspector - PlayerController:
- Ground Layer: "Default" (أو Layer يحتوي الأرضية)

---

## المرحلة الرابعة: إعداد العدو

### 11. إنشاء كائن العدو
```
Right Click → 3D Object → Cube
الاسم: Enemy
```

### 12. تكوين مكونات العدو
```
1. Transform:
   - Position: (3, 1, 0)
   - Scale: (0.7, 1.1, 0.5)

2. Rigidbody:
   - Mass: 1.2
   - Drag: 0
   - Angular Drag: 0.05
   - Freeze Rotation: (X, Y, Z) ✓ All
   - Collision Detection: Continuous

3. Box Collider:
   - Size: (0.7, 1.1, 0.5)
   - Is Trigger: ✗

4. Layer: "Enemy"
```

### 13. إضافة Scripts للعدو
1. أضف Component: `EnemyController.cs`
2. أضف Component: `CharacterModel.cs`
   - عين `isPlayer = false`

### 14. تكوين المراجع في EnemyController
في Inspector - EnemyController:
- Player Layer: "Player"
- Ground Layer: "Default"

---

## المرحلة الخامسة: إعداد الواجهة

### 15. إنشاء Canvas
```
Right Click → UI → Canvas
```

### 16. إنشاء عناصر الواجهة

#### شريط صحة اللاعب:
```
Canvas → Right Click → UI → Image Panel
الاسم: PlayerHealthBar
- Anchor Presets: Top Left
- Position: (100, -50)
- Size: (300, 30)
- Color: Blue
```

#### شريط صحة العدو:
```
Canvas → Right Click → UI → Image Panel
الاسم: EnemyHealthBar
- Anchor Presets: Top Right
- Position: (-100, -50)
- Size: (300, 30)
- Color: Red
```

#### نصوص الصحة:
```
Canvas → Right Click → UI → Text
الاسم: PlayerHealthText
- Anchor Presets: Top Left
- Position: (100, -100)
- Content: "Player: 100 / 100"

Canvas → Right Click → UI → Text
الاسم: EnemyHealthText
- Anchor Presets: Top Right
- Position: (-100, -100)
- Content: "Enemy: 80 / 80"
```

#### حالة اللعبة:
```
Canvas → Right Click → UI → Text
الاسم: GameStatus
- Anchor Presets: Center
- Font Size: 60
- Color: White
- Content: ""
```

### 17. إضافة UIManager
```
Right Click → Create Empty
الاسم: UIManager
أضف Component: UIManager.cs
```

### 18. ربط المراجع
في Inspector - UIManager:
- Player: (drag اللاعب)
- Enemy: (drag العدو)
- Player Health Bar: (drag PlayerHealthBar)
- Enemy Health Bar: (drag EnemyHealthBar)
- Player Health Text: (drag PlayerHealthText)
- Enemy Health Text: (drag EnemyHealthText)
- Game Status Text: (drag GameStatus)

---

## المرحلة السادسة: الاختبار النهائي

### 19. إنشاء Layers
```
Edit → Project Settings → Layers
أضف Layers:
- "Player"
- "Enemy"
- "Ground"
```

### 20. اختبار اللعبة
```
اضغط Play ▶️
```

**تحكم اللعبة:**
- W/A/S/D - حركة
- Space - قفز
- Left Click - لكم

---

## ⚙️ التخصيص (اختياري)

### في PlayerController Inspector:
- **Move Speed**: سرعة الحركة (افتراضي: 5)
- **Jump Force**: قوة القفز (افتراضي: 5)
- **Punch Damage**: ضرر الكم (افتراضي: 10)
- **Max Health**: الصحة الكاملة (افتراضي: 100)

### في EnemyController Inspector:
- **Detection Range**: نطاق الكشف (افتراضي: 15)
- **Move Speed**: سرعة المطاردة (افتراضي: 3.5)
- **Attack Damage**: ضرر الهجوم (افتراضي: 15)
- **Max Health**: الصحة الكاملة (افتراضي: 80)

---

## 🐛 استكشاف الأخطاء

| المشكلة | الحل |
|--------|------|
| اللاعب لا يتحرك | تأكد من أن Rigidbody غير kinematic |
| لا يمكن الكم | تأكد من أن العدو معه Tag = "Enemy" |
| العدو لا يتحرك | تأكد من أن Player Layer صحيح |
| أشرطة الصحة فارغة | تأكد من ربط المراجع في UIManager |

---

**تم! لعبتك جاهزة للعب!** 🎮✨
