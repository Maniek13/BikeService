import React, { Component } from 'react';
import {View, Text, Button, BackHandler} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import User from '../objects/User'

class ControllPanelScreen extends Component {
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

  async logOut(){
    await AsyncStorage.removeItem('@BikeServiceUser');

    User.user.Id = 0;
    this.props.navigate('Main');
  }
  
  render() {
    return (
      <View>
        <Button 
          onPress={this.logOut.bind(this)}
          title="Wyloguj"
        />
        <Text>tasks</Text>
      </View>
    );
  }
}

export default ControllPanelScreen;

