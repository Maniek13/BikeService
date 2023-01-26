import React, { Component } from 'react';
import {Text, View, StyleSheet} from 'react-native';

import Task from '../objects/Task';
import Settings from '../objects/Settings';

import MainStyles from '../styles/MainStyles';
import UserTaskStyles from '../styles/UserTaskStyles';

class UserTask extends Component {
    constructor(props){
        super(props);
    }

  render() {
    return (
        <View style={userTaskStyle.conteiner}>
            <Text style={{color: '#000000', fontSize: 23, textAlign: 'center'}} name='title'>{this.props.task.Header}</Text>
            <Text style={mainStyle.text} name='description'>{this.props.task.Description}</Text>
            <Text style={mainStyle.text} name='state'>{String(Task.statusList.find(x => x.Value === this.props.task.State).Label)}</Text>
        </View>
    );
  }
}

export default UserTask;

const mainStyle = MainStyles[Settings.SchemaStyle]
const userTaskStyle = UserTaskStyles[Settings.SchemaStyle]
