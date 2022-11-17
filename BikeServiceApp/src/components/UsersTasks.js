import React, { Component } from 'react';
import {Text, TextInput, Button, View} from 'react-native';

import Task from '../objects/Task'
import UserTask from '../components/UserTask';

class UsersTasks extends Component {
  constructor(props){
    super(props);

    this.state = {
      taskNumber: 0,
      showTask: false,
      task: Task.task
    };
  }
  
  search(){
    this.setState({ showTask: false });
    this.setState({
      task:{
        Id: 1,
        Header: 'Tytuł',
        Description: 'Opis',
        State: '0'
    }});
    this.setState({ showTask: true });
  }

  render() {
    return (
      <View>
        <Text>Wyszukiwarka zleceń</Text>
        <TextInput 
          placeholder="Numer zlecenia" 
          onChange={number => this.setState({taskNumber: number})}
        />
        <Button
          onPress={this.search.bind(this)}
          title="Wyszukaj"
        />
         {this.state.showTask === true ? <UserTask task = {this.state.task}/> : ""}
      </View>
       
    );
  }
}

export default UsersTasks;