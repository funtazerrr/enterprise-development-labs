/// <summary>
/// Шкала оценки физического состояния товара
/// </summary>
public enum Condition
{
    /// <summary> Абсолютно новый, запечатанный </summary>
    New,
    /// <summary> Б/у, без видимых дефектов </summary>
    Excellent,
    /// <summary> Незначительные следы использования </summary>
    Good,
    /// <summary> Заметные повреждения, но пригоден для прослушивания </summary>
    Satisfactory,
    /// <summary> Серьёзные повреждения, возможны дефекты звука </summary>
    Poor
}