import React, {Component} from 'react';
import {View, StyleSheet, Text, BackHandler} from 'react-native';

import Task from '../objects/Task'

class EditTaskScreen extends Component {
  constructor(props){
    super(props);

    this.state = {
      task: Task.task
    };
  }


  render() {
    return (
      <View >
        <Text>{this.state.task.Header}</Text>
      </View>
    );
  }
}

export default EditTaskScreen;

const styles = StyleSheet.create({
  text : {
    color: '#000000',
    textAlign: 'center',
    fontSize: 20,
    marginTop: 10,
    marginBottom: 10
  }
});

