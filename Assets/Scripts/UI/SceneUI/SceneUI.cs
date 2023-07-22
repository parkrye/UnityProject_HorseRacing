public abstract class SceneUI : BaseUI
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void OnEnable()
    {
        Initialize();
    }

    public virtual void Initialize()
    {

    }
}
