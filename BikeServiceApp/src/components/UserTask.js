import React, { Component } from 'react';
import {Text, View, StyleSheet} from 'react-native';

class UserTask extends Component {
    constructor(props){
        super(props)
    }

  render() {
    return (
        <View>
            <Text style={styles.text} name='title'>{this.props.task.Header}</Text>
            <Text style={styles.text} name='description'>{this.props.task.Description}</Text>
            <Text style={styles.text} name='state'>{this.props.task.State}</Text>
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
  }
});
