import React, { Component } from 'react';
import {Text, TextInput, Button, View, StyleSheet, TouchableOpacity, RefreshControl} from 'react-native';

import UserTask from '../components/UserTask';
import Error from '../components/Error';
import TasksController from '../controllers/TasksController';
import Response from '../objects/Response';

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

          console.log(Response.response.data)
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
        <Text style={styles.text}>Wyszukiwarka zleceń</Text>
        <TextInput 
          style={styles.textInput}
          placeholder="Numer zlecenia" 
          placeholderTextColor="gray" 
          onChangeText={number => this.setState({taskNumber: number})}
        />
        <TouchableOpacity style={this.state.btnSearchDisabled ? styles.searchButtonDisabled : styles.searchButton } onPress={this.search.bind(this)} disabled={this.state.btnLoginDisabled}>
          <Text style={styles.buttonText}>Wyszukaj</Text>
        </TouchableOpacity>
        {this.state.showTask === true ? <UserTask task = {this.state.task}/> : ''}
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
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
    marginBottom: 10,
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
  searchButtonDisabled: {
    alignItems: 'center',
    marginLeft:'auto',
    marginRight:'auto',
    justifyContent: 'center',
    width: 100,
    padding: 5,
    backgroundColor: 'grey',
    borderRadius: 5,
    zIndex: 100
  },
  buttonText : {
    color: 'white',
    textAlign: 'center'
  },
});
