import React, { Component } from 'react';
import {Text, TextInput, Button, View, StyleSheet, TouchableOpacity, RefreshControl} from 'react-native';

import UserTask from '../components/UserTask';
import Error from '../components/Error';
import TasksController from '../controllers/TasksController';
import Response from '../objects/Response';

import mainStyle from '../styles/MainStyle';

class UsersTasks extends Component {
  constructor(props){
    super(props);

    this.state = {
      taskNumber: 0,
      showTask: false,
      task: {},
      showError: false,
      error: {},
      btnSearchDisabled: false 
    };
  }
  
  async search(){
    this.setState({ showTask: false });
    this.setState({ showError: false });
    this.setState({ btnSearchDisabled: true });

    await TasksController.getTask(this.state.taskNumber);
    let onTime = setInterval(() => {
      if(Response.response.code !== 0){
        if(Response.response.code === 1){
          this.setState({
            task: Response.response.data
          });
          this.setState({ showTask: true });
        }
        else{
          this.setState({
            error: Response.response
          });
          this.setState({ showError: true });
        }

        Response.response = {
          code: 0,
          data: {
            message: ''
          }
        }

        this.setState({ btnSearchDisabled: false });
        clearInterval(onTime);
      }
    }, 100);
  }

  render() {
    return (
      <View>
        <Text style={mainStyle.text}>Wyszukiwarka zleceń</Text>
        <TextInput 
          style={styles.textInput}
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

const styles = StyleSheet.create({
  textInput :{
    color: '#000000',
    borderWidth: 1,
    marginTop: 10,
    marginLeft: 'auto',
    marginRight: 'auto',
    width: 200,
    padding: 2,
    backgroundColor: 'white'
  }
});
