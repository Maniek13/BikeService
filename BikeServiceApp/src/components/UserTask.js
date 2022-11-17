import React, { Component } from 'react';
import {Text, View} from 'react-native';

class UserTask extends Component {
    constructor(props){
        super(props)
    }

  render() {
    return (
        <View>
            <Text name='title'>{this.props.task.Header}</Text>
            <Text name='description'>{this.props.task.Description}</Text>
            <Text name='state'>{this.props.task.State}</Text>
        </View>
    );
  }
}

export default UserTask;