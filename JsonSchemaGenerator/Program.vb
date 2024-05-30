Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module JsonSchemaGenerator

    Public Function GenerateSchema(jsonData As String) As String
        ' Parse JSON & Create Schema
        Dim token As JToken = JToken.Parse(jsonData)
        Dim schema As JObject = New JObject()
        ProcessToken(token, schema)

        Return schema.ToString(Formatting.Indented)
    End Function

    Private Sub ProcessToken(token As JToken, schema As JObject)
        ' Check Data Type & Cast
        Select Case token.Type
            ' Struct
            Case JTokenType.Object
                Dim obj As JObject = CType(token, JObject)
                For Each prop As JProperty In obj.Properties()
                    Dim propSchema As JObject = New JObject()
                    ProcessToken(prop.Value, propSchema)
                    schema(prop.Name) = propSchema
                Next
            ' Array
            ' Create Schema with First Index
            Case JTokenType.Array
                Dim array As JArray = CType(token, JArray)
                If array.Count > 0 Then
                    Dim elementSchema As JObject = New JObject()
                    ProcessToken(array(0), elementSchema)
                    schema("type") = "array"
                    schema("items") = elementSchema
                Else
                    schema("type") = "array"
                    schema("items") = New JObject()
                End If
            ' Others
            Case JTokenType.Integer
                schema("type") = "integer"
            Case JTokenType.Float
                schema("type") = "number"
            Case JTokenType.String
                schema("type") = "string"
            Case JTokenType.Boolean
                schema("type") = "boolean"
            Case JTokenType.Null
                schema("type") = "null"
            Case Else
                schema("type") = "string"
        End Select
    End Sub
End Module

' TEST
Module TestModule
    Sub Main()
        Dim jsonData As String = "
        {
            ""version"": ""0.0.1"",
            ""releaseDate"": ""2024-05-29"",
            ""writer"": ""hooniegit"",
            ""sources"": [
                {
                    ""source"": ""SampleSource"",
                    ""sets"": [
                        {
                            ""model"": ""SampleModel01"",
                            ""tags"": [
                                {
                                    ""name"": ""SampleTagOff"",
                                    ""condition"": {},
                                    ""type"": ""Offline""
                                },
                                {
                                    ""name"": ""SampleTag02"",
                                    ""condition"": {
                                        ""upper"": 100
                                    },
                                    ""type"": ""Normal""
                                },
                                {
                                    ""name"": ""1EL-GENMWD-I_TBN"",
                                    ""condition"": {
                                        ""upper"": 50,
                                        ""lower"": 10
                                    },
                                    ""type"": ""Normal""
                                }
                            ]
                        }
                    ]
                }
            ]
        }"
        Dim schema As String = JsonSchemaGenerator.GenerateSchema(jsonData)
        Console.WriteLine(schema)
    End Sub
End Module
