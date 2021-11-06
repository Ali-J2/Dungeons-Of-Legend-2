namespace SheetCodes
{
	//Generated code, do not edit!

	public enum SceneIdentifier
	{
		[Identifier("None")] None = 0,
		[Identifier("L1")] L1 = 1,
		[Identifier("L2")] L2 = 2,
		[Identifier("L3")] L3 = 3,
	}

	public static class SceneIdentifierExtension
	{
		public static SceneRecord GetRecord(this SceneIdentifier identifier, bool editableRecord = false)
		{
			return ModelManager.SceneModel.GetRecord(identifier, editableRecord);
		}
	}
}
