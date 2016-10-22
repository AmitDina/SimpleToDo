# Simple TODO  - API


Simple TODO  is an **REST Web API** 

**Using**

* ASP.NET Web API 2
* [Simple Injector DI](https://simpleinjector.org/index.html)
* [JSON.NET](http://www.newtonsoft.com/json)
* [Microsoft Owin](https://www.asp.net/aspnet/overview/owin-and-katana)

![TODO APi1](http://i.imgur.com/aJddCAL.png)

![Imgur](http://i.imgur.com/S58BpOs.png)

![Imgur](http://i.imgur.com/6UnQc6d.png)

## API Reference 

**GET REQUEST**

 `Get all Todo's with all tasks`  

```
http://localhost:51573/api/v1/
```

 `Get all Todo's`  

```
http://localhost:51573/api/v1/simpletodo
```

 `Get single Todo item`  

```
http://localhost:51573/api/v1/simpletodo/{id}
```
 `Get single Todo item and its tasks`  

```
http://localhost:51573/api/v1/simpletodo/{id}/tasks
```
**POST REQUEST**

 `Add new Todo list  `  

```
http://localhost:51573/api/v1/simpletodo/add

ToDoItem: {
	ToDoItemId: "285e0193-fc4c-4aaf-943b-c7f76c79ef95",
	Description: "Amit's todo",
	SortOrder: 10
}
```

 `Add task/tasks to an existing Todo item`  

```
http://localhost:51573/api/v1/simpletodo/{id}/add

SimpleTasks: [
	{
		ToDoItemId: "565388fa-4f22-464f-8e0c-35dbded4bd4c",
		SimpleTaskId: "f4a42ba8-940b-4c9f-b00f-a7fc5a9267c0",
		Title: "Tesco online",
		Status: 0,
		DateAdded: "2016-10-21T19:17:41.654163+01:00",
		LastUpdated: null,
		DueDate: null,
		Notes: "Order online"
	},
	{
		ToDoItemId: "565388fa-4f22-464f-8e0c-35dbded4bd4c",
		SimpleTaskId: "c0766f88-2150-4445-89d7-d20e787271bf",
		Title: "Car MOT",
		Status: 0,
		DateAdded: "2016-10-21T19:17:41.654163+01:00",
		LastUpdated: null,
		DueDate: null,
		Notes: "car"
	},
]
```

 `Get single Todo item`  

```
http://localhost:51573/api/v1/simpletodo/{id}
```
 `Get single Todo item and its tasks`  

```
http://localhost:51573/api/v1/simpletodo/{id}/tasks
```



## Bugs and enhancements 

*  DELETE PUT not implemented


## Social 

* https://twitter.com/amit_pore
