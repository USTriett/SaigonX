using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    private static string httpPrefix = "http://";
    private static int port = 8000;
    private static string auth = "/api/auth";
    public static string LOGIN = httpPrefix + "localhost:" + port + auth + "/login";

    public static string REGISTER = httpPrefix + "localhost:" + port + auth + "/register";
}
