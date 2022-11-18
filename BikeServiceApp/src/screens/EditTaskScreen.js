import React, {Component} from 'react';
import {View, StyleSheet, Text, BackHandler} from 'react-native';

import Task from '../objects/Task'

class EditTaskScreen extends Component {
  constructor(props){
    super(props);

    this.handleBackButton.bind(this)

    this.state = {
      task: {}
    };
  }

  componentDidMount(){
    this.focusListener = this.props.navigation.addListener('focus', () => {
      this.setState({ task: Task.task});
    });
  }
  
  handleBackButton(){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    this.props.navigation.navigate('ControllPanel');
    return true;
  } 

  componentWillUnmount() {
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
  }

  render() {
    return (
      <View >
        <Text style={styles.text}>{this.state.task.Header}</Text>
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

