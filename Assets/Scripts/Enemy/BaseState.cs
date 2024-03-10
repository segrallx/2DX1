//

public abstract class BaseState
{

    protected Enemy mCurEnemy;

    public abstract void OnEnter(Enemy enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
