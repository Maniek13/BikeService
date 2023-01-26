import React, { Component } from 'react';
import {Text, TextInput, Button, View, StyleSheet, TouchableOpacity, RefreshControl} from 'react-native';

import UserTask from '../components/UserTask';
import Error from '../components/Error';
import TasksController from '../controllers/TasksController';
import Response from '../objects/Response';
import Settings from '../objects/Settings';

import MainStyles from '../styles/MainStyles';
import UsersTasksStyles from '../styles/UsersTasksStyles';

class UsersTasks extends Component {
  constructor(props){
    super(props);
    this.state = {
      taskNumber: "",
      showTask: false,
      task: {},
      showError: false,
      error: {},
      btnSearchDisabled: false 
    };
  }

  onEndLoading(statment){
    if(statment === "if"){
      this.setState({
        task: Response.response.data
      });
      this.setState({ showTask: true });
      this.setState({ btnSearchDisabled: false });
    }

    if(statment === "else"){
      this.setState({
        error: Response.response
      });
      this.setState({ showError: true });
      this.setState({ btnSearchDisabled: false });
    }
  }
  
  async search(){
    this.setState({ showTask: false });
    this.setState({ showError: false });
    this.setState({ btnSearchDisabled: true });

    Response.response = {
      code: 0,
      data: {
        message: ''
      }
    }

    await TasksController.getTask(this.state.taskNumber);
    Response.getDate(this.onEndLoading.bind(this));
  }

  render() {
    return (
      <View>
        <Text style={mainStyle.text}>Wyszukiwarka zleceń</Text>
        <TextInput 
          style={userTaskStyle.textInput}
          placeholder="Numer zlecenia" 
          placeholderTextColor="gray" 
          onChangeText={number => this.setState({taskNumber: number})}
        />
        <TouchableOpacity style={this.state.btnSearchDisabled ? mainStyle.buttonDisabled : mainStyle.buttonEnabled } onPress={this.search.bind(this)} disabled={this.state.btnLoginDisabled}>
          <Text style={mainStyle.buttonText}>Wyszukaj</Text>
        </TouchableOpacity>
        {this.state.showTask === true ? <UserTask task = {this.state.task}/> : ''}
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
      </View>
    );
  }
}

export default UsersTasks;

const mainStyle = MainStyles[Settings.SchemaStyle]
const userTaskStyle = UsersTasksStyles[Settings.SchemaStyle]