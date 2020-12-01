using Newtonsoft.Json;

public abstract class DbBaseEntity
{
    [JsonProperty]
    public int Id { get; protected set; } // Note that this field will probably cause Unity json problems later when serializing/deserializing.

    /// <summary>
    /// Returns true if the Id of this object is 'valid'.
    /// </summary>
    public bool IsMyIdValid { get { return IsIdValid(this.Id); } }

    /// <summary>
    /// The Id that should be used when assigning or comparing for an invalid Id.
    /// Due to errors and such other invalid Id's could also be possible.
    /// </summary>
    public const int InvalidId = -1;

    /// <summary>
    /// The first Id that is considered to be invalid. Everything higher than this Id is considered a valid Id.
    /// </summary>
    public const int FirstValidId = 1;

    /// <summary>
    /// Assigns an InvalidId to this entity.
    /// </summary>
    protected void SetInvalidId()
    {
        SetId(InvalidId);
    }

    /// <summary>
    /// Assigns a new Id to this entity. Use with common sense because Id's are mostly not
    /// supposed to be altered but sometimes they must be initialized from the outside or something.
    /// </summary>
    public void SetId(int id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Returns true if the id is considered to be a 'valid' one.
    /// </summary>
    public static bool IsIdValid(int id)
    {
        return id >= FirstValidId;
    }
}
