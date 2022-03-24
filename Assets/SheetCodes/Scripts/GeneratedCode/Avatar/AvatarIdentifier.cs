namespace SheetCodes
{
	//Generated code, do not edit!

	public enum AvatarIdentifier
	{
		[Identifier("None")] None = 0,
	}

	public static class AvatarIdentifierExtension
	{
		public static AvatarRecord GetRecord(this AvatarIdentifier identifier, bool editableRecord = false)
		{
			return ModelManager.AvatarModel.GetRecord(identifier, editableRecord);
		}
	}
}
