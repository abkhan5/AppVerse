﻿using System.ComponentModel;

namespace AppVerse;

public class AppVerseErrorRegistry
{
    /// <summary>
    ///     User does not exist
    /// </summary>
    [Description(ErrorAuthMessage101)] public const string ErrorAuth101 = "ErrorAuth101";

    private const string ErrorAuthMessage101 = "User does not exist";


    /// <summary>
    ///     Username or Password is incorrect
    /// </summary>
    [Description(ErrorAuthMessage102)] public const string ErrorAuth102 = "ErrorAuth102";

    private const string ErrorAuthMessage102 = "Username or Password is incorrect";


    /// <summary>
    ///     Username is already taken
    /// </summary>
    [Description(ErrorAuthMessage103)] public const string ErrorAuth103 = "ErrorAuth103";

    private const string ErrorAuthMessage103 = "Username is already taken";


    /// <summary>
    ///     Email is not authenticated
    /// </summary>
    [Description(ErrorAuthMessage423)] public const string ErrorAuth423 = "ErrorAuth423";

    private const string ErrorAuthMessage423 = "Email is not authenticated";

    [Description(ErrorAuthMessage105)] public const string UnAuthorizedError = "UnAuthorizedError";

    private const string ErrorAuthMessage105 = "User is not Authorized";


    [Description(ErrorAuthMessage106)] public const string ErrorAuth106 = "ErrorAuth106";

    private const string ErrorAuthMessage106 = "Refresh Token is invalid";


    /// <summary>
    ///     Email is not authenticated
    /// </summary>
    [Description(ErrorAuthMessage107)] public const string ErrorAuth107 = "ErrorAuth107";

    private const string ErrorAuthMessage107 = "The new password cannot be the same as the old";


    /// <summary>
    ///     Email is not authenticated
    /// </summary>
    [Description(ErrorAuthMessage108)] public const string ErrorAuth108 = "ErrorAuth108";

    private const string ErrorAuthMessage108 = "This email is taken.Try another.";


    /// <summary>
    ///     Email is not authenticated
    /// </summary>
    [Description(ErrorAuthMessage109)] public const string ErrorAuth109 = "ErrorAuth109";

    private const string ErrorAuthMessage109 = "Account Creation time has expired. Please try again";


    /// <summary>
    ///     Email is not authenticated
    /// </summary>
    [Description(ResourceNotFoundMessage)] public const string ResourceNotFound = "ResourceNotFound204";

    private const string ResourceNotFoundMessage = "Resource does not exist";

    public static Dictionary<string, string> ErrorCatalog = new()
    {
        { ErrorAuth101, ErrorAuthMessage101 },
        { ErrorAuth102, ErrorAuthMessage102 },
        { ErrorAuth103, ErrorAuthMessage103 },
        { ErrorAuth423, ErrorAuthMessage423 },
        { UnAuthorizedError, ErrorAuthMessage105 },
        { ErrorAuth106, ErrorAuthMessage106 },
        { ErrorAuth107, ErrorAuthMessage107 },
        { ErrorAuth108, ErrorAuthMessage108 },
        { ErrorAuth109, ErrorAuthMessage109 },
        { ResourceNotFound, ResourceNotFoundMessage }
    };
}