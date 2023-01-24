import React, { Component } from 'react';
import {Text, View, StyleSheet} from 'react-native';

import Task from '../objects/Task'

import mainStyle from '../styles/MainStyle';

class UserTask extends Component {
    constructor(props){
        super(props)
    }

  render() {
    return (
        <View style={styles.conteiner}>
            <Text style={{color: '#000000', fontSize: 23, textAlign: 'center'}} name='title'>{this.props.task.Header}</Text>
            <Text style={mainStyle.text} name='description'>{this.props.task.Description}</Text>
            <Text style={mainStyle.text} name='state'>{String(Task.statusList.find(x => x.Value === this.props.task.State).Label)}</Text>
        </View>
    );
  }
}

export default UserTask;

const styles = StyleSheet.create({
  conteiner:{
    borderWidth: 1,
    borderRadius: 10,
    marginLeft: 'auto',
    marginRight: 'auto',
    width: 250,
    marginTop: 20,
    padding: 10,
    backgroundColor: 'white'
  }
});
