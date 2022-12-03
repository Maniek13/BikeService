import React, { Component } from 'react';
import {Text, View, StyleSheet} from 'react-native';

import Task from '../objects/Task'

class UserTask extends Component {
    constructor(props){
        super(props)
    }

  render() {
    return (
        <View style={styles.conteiner}>
            <Text style={{color: '#000000', fontSize: 23, textAlign: 'center'}} name='title'>{this.props.task.header}</Text>
            <Text style={styles.text} name='description'>{this.props.task.description}</Text>
            <Text style={styles.text} name='state'>{String(Task.statusList.find(x => x.Value === this.props.task.state).Label)}</Text>
        </View>
    );
  }
}

export default UserTask;

const styles = StyleSheet.create({
  text : {
    color: '#000000',
    textAlign: 'center',
    marginTop: 10
    
  },
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
