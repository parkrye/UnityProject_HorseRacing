using System.Collections;

public class MainScene : BaseScene
{
    protected override IEnumerator LoadingRoutine()
    {
        yield return null;

        Progress = 1f;
    }
}
