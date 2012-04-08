namespace RecursiveReflection
{
	public interface IRecursiveReflector
	{
		T Reflect<T>(T model, string culture) where T : class;
	}
}