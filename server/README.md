If the file is named Task, when referencing in another class, use Models.Task to avoid the reserved keyword Task
When setting up endpoints, if there are 2 get method in a controller, this will cause confusion. => Resolve by use 2 different [HttpGet("id")]
