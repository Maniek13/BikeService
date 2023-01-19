import React, {Component} from 'react';
import {View, Text, TouchableOpacity, Alert, TextInput, StyleSheet} from 'react-native';

import TasksController from '../controllers/TasksController';
import Task from '../objects/Task';
import Error from '../components/Error';
import Response from '../objects/Response';

class AddTaskScreen extends Component {
  constructor(props){
    super(props);

    this.state = {
      header: '',
      description: '',
      showError: false,
      error: {}    
    };
  }

  async addTask(){
    let task = {
      TaskId: 0,
      Header: this.state.header,
      Description: this.state.description,
      State: 1,
    };
    Task.task = task;
    
    Response.response = {
      code: 0,
      data: {
        message: ''
      }
    }
    
    await TasksController.addTask();
    let onTime = setInterval(() => {
      if(Response.response.code !== 0){
        if(Response.response.code === 1){
          TasksController.tasksList.push(Response.response.data)
          this.props.navigation.navigate('ControllPanel');
        }
        else{
          this.setState({
            error: Response.response
          });
          this.setState({ showError: true });
        }
        clearInterval(onTime);
      }
    }, 100);
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
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
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


