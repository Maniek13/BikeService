import React, {Component} from 'react';
import {View, Text, TouchableOpacity, Alert, TextInput, StyleSheet} from 'react-native';

import TasksController from '../controllers/TasksController';
import Task from '../objects/Task';
import Error from '../components/Error';
import Response from '../objects/Response';
import Settings from '../objects/Settings';

import MainStyles from '../styles/MainStyles';

class AddTaskScreen extends Component {
  constructor(props){
    super(props);
    this.state = {
      header: '',
      description: '',
      showError: false,
      btnLoginDisabled: false,
      error: {}    
    };
  }

  onEndLoading(statment){
    if(statment === "if"){
      TasksController.tasksList.push(Response.response.data);
      this.props.navigation.navigate('ControllPanel');
    }

    if(statment === "else"){
      this.setState({
        error: Response.response
      });
      this.setState({ showError: true });
      this.setState({ btnLoginDisabled: false});
    }
  }

  async addTask(){
    this.setState({ showError: false });
    this.setState({ btnLoginDisabled: true});

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
    Response.getDate(this.onEndLoading.bind(this));
  }

  render() {
    return (
      <View >
        <TextInput 
          style={mainStyle.taskHeader}
          placeholder="tytuł" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({header: newText})}
        />
        <TextInput 
          multiline={true}
          style={mainStyle.taskDescription}
          placeholder="opis" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({description: newText})}
        />
        <TouchableOpacity style={this.state.btnLoginDisabled ? mainStyle.buttonDisabled : mainStyle.buttonEnabled } onPress={this.addTask.bind(this)} disabled={this.state.btnLoginDisabled}>
            <Text style={mainStyle.buttonText}>Dodaj</Text>
        </TouchableOpacity>
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
      </View>
    );
  }

}

export default AddTaskScreen;

const mainStyle = MainStyles[Settings.SchemaStyle]