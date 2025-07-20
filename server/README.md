If the file is named Task, when referencing in another class, use Models.Task to avoid the reserved keyword Task
When setting up endpoints, if there are 2 get method in a controller, this will cause confusion. => Resolve by use 2 different [HttpGet("id")]
When encountering this error "Cannot insert explicit value for identity column in table 'activities' when IDENTITY_INSERT is set to OFF.", it means that the record we're trying to insert contains the id, the id is generated automatically by the sql server.
