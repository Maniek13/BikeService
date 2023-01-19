class Task{
    static task = {
        taskID: 0,
        header: "",
        description: "",
        state: 0,
        taskIDKey: "",
        appID: 0
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
