import React, { Component } from 'react';
import {View, Text, Button, BackHandler} from 'react-native';

class AdministrationListOfTasks extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);
  }
  componentDidMount (){
    BackHandler.addEventListener('hardwareBackPress', this.handleBackButton);
  }

  handleBackButton(){
    this.props.navigate('Main');
    return true;
  } 
  
  render() {
    return (
      <View>
        <Text>tasks</Text>
      </View>
    );
  }
}

export default AdministrationListOfTasks;

