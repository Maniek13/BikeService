class Task{
    static task = {
        TaskId: 0,
        Header: "",
        Description: "",
        State: 0,
        TaskIdKey: "",
        AppId: 0,
        InitDate: ""
    }

    static statusList = [
        {   
            Value : 0,
            Label: 'Brak danych'
        },
        {   
            Value : 1,
            Label: 'Przyjęte'
        },
        {   
            Value : 2,
            Label: 'W trakcie'
        },
        {   
            Value : 3,
            Label: 'Gotowe'
        },
        {   
            Value : 4,
            Label: 'Odebrane'
        }
    ]

    
}

export default Task;
