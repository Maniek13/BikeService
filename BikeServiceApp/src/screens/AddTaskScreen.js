import React, {Component} from 'react';
import {View, Text, TouchableOpacity, Alert, TextInput, StyleSheet} from 'react-native';

import TasksController from '../controllers/TasksController'
import Task from '../objects/Task'

class AddTaskScreen extends Component {
  constructor(props){
    super(props);

    this.state = {
      header: '',
      description: ''
    };
  }

  addTask(){
    let task = {
      Id: 0,
      Header: this.state.header,
      Description: this.state.description,
      State: 1,
    };
    Task.task = task;
    
    let res = TasksController.addTask();
    
    if(res.code === 200){
      this.tasksList.push({
        Id: res.data.id,
        Header: Task.task.Header,
        Description: Task.task.Description,
        State: 1
      })
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
        <TextInput 
          style={styles.header}
          placeholder="tytuł" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({header: newText})}
        />
        <TextInput 
          multiline={true}
          style={styles.description}
          placeholder="opis" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({description: newText})}
        />
         <TouchableOpacity style={styles.searchButton} onPress={this.addTask.bind(this)}>
            <Text style={styles.buttonText}>Dodaj</Text>
        </TouchableOpacity>
      </View>
    );
  }

}

export default AddTaskScreen;

const styles = StyleSheet.create({
  header:{
    color: '#000000',
    borderWidth: 1,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginTop: 10,
    width: 300,
    padding: 5,
    backgroundColor: 'white',
    height: 30
  },
  description:{
    color: '#000000',
    borderWidth: 1,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginTop: 10,
    width: 300,
    padding: 5,
    backgroundColor: 'white',
    height: 'auto',
    textAlignVertical: "top"
  },
  searchButton: {
    alignItems: 'center',
    marginTop: 10,
    marginLeft:'auto',
    marginRight:'auto',
    justifyContent: 'center',
    width: 100,
    padding: 5,
    backgroundColor: '#249ef0',
    borderRadius: 5,
    zIndex: 100
  },
  buttonText:{
    color: 'white'
  }
});


