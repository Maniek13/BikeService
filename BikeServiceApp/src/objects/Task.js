class Task{
    static task = {
        Id: 0,
        Header: "",
        Description: "",
        State: 0
    }

    static statusList = [
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
