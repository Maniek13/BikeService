class Task{
    static task = {
        Id: 0,
        Header: "",
        Description: "",
        State: 0
    }

    static statusList = [
        {   
            Id : 1,
            State: 'Przyjęte'
        },
        {   
            Id : 2,
            State: 'W trakcie'
        },
        {   
            Id : 3,
            State: 'Gotowe'
        },
        {   
            Id : 4,
            State: 'Odebrane'
        }
    ]

    
}

export default Task;
