public struct PigeonMenuObject : IMenuObject
{
    public int index;

    public MenuObject GetObjectType()
    {
        return MenuObject.Pigeon;
    }

    public PigeonMenuObject(int index)
    {
        this.index = index;
    }
}
