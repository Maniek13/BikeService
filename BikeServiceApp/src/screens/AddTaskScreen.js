import React, {Component} from 'react';
import {View, Button, Alert} from 'react-native';

import TasksController from '../controllers/TasksController'

class AddTaskScreen extends Component {
  constructor(props){
    super(props);
  }

  addTask(){
    var status = TasksController.addTask();
    if(status === true){
        this.props.navigation.navigate('ControllPanel');
    }
    else{
        Alert.alert(
            "Dodawanie zlecenia",
            "Błąd" + status,
            [
              { text: "OK" }
            ]
          );
    }
  }

  render() {
    return (
      <View >
        <Button 
            onPress={this.addTask.bind(this)}
            title='Dodaj'
        />
      </View>
    );
  }

}

export default AddTaskScreen;


