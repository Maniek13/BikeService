import React, { Component } from 'react';
import {Text, TextInput, Button, View, StyleSheet, TouchableOpacity} from 'react-native';

import UserTask from '../components/UserTask';
import TasksController from '../controllers/TasksController';

class UsersTasks extends Component {
  constructor(props){
    super(props);

    this.state = {
      taskNumber: 0,
      showTask: false,
      task: {}
    };
  }
  
  search(){
    this.setState({ showTask: false });
    let task = TasksController.getTask();
    this.setState({
      task: task
    });

    if(task.Id !== 0){
      this.setState({ showTask: true });
    }
  }

  render() {
    return (
      <View>
        <Text style={styles.text}>Wyszukiwarka zleceń</Text>
        <TextInput 
          style={styles.textInput}
          placeholder="Numer zlecenia" 
          placeholderTextColor="gray" 
          onChange={number => this.setState({taskNumber: number})}
        />
        <TouchableOpacity style={styles.searchButton} onPress={this.search.bind(this)}>
          <Text style={styles.buttonText}>Wyszukaj</Text>
        </TouchableOpacity>
         {this.state.showTask === true ? <UserTask task = {this.state.task}/> : ""}
      </View>
       
    );
  }
}

export default UsersTasks;

const styles = StyleSheet.create({
  text : {
    color: '#000000',
    textAlign: 'center',
    fontSize: 20,
    marginTop: 10
  },
  textInput :{
    color: '#000000',
    borderWidth: 1,
    marginTop: 10,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginBottom: 20,
    width: 200,
    padding: 2,
    backgroundColor: 'white'
  },
  searchButton: {
    alignItems: 'center',
    marginLeft:'auto',
    marginRight:'auto',
    justifyContent: 'center',
    width: 100,
    padding: 5,
    backgroundColor: '#249ef0',
    borderRadius: 5,
    zIndex: 100
  },
  buttonText : {
    color: 'white',
    textAlign: 'center'
  },
});
