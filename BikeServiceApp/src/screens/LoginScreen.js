import React, { Component } from 'react';
import { View, Text, Button,} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import User from '../objects/User'

class LoginScreen extends Component {
  constructor(props){
    super(props);
  }

  async logIn(){
    await AsyncStorage.setItem('@BikeServiceUser', '1')
    User.user.Id = 1;
    this.props.navigate('ControllPanel');
  }

  render() {
    return (
      <View >
        <Text>Login</Text>
        <Button 
          onPress={this.logIn.bind(this)}
          title="Zaloguj"
        />
      </View>
    );
  }
}

export default LoginScreen;


