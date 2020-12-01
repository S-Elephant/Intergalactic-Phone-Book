using System;

/* All public custom exceptions go into this file. */

[Serializable]
public class CaseStatementMissingException : Exception
{
    public CaseStatementMissingException() { }

    public CaseStatementMissingException(object receivedValue)
        : base(String.Format("Case statement missing: {0}", receivedValue)) { }
}
