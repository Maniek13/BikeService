import React, { Component } from 'react';
import { View, Text, Button, StyleSheet} from 'react-native';

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
        <Text style={styles.text}>Login</Text>
        <Button 
          onPress={this.logIn.bind(this)}
          title="Zaloguj"
        />
      </View>
    );
  }
}

export default LoginScreen;


const styles = StyleSheet.create({
  text : {
    color: '#000000',
    textAlign: 'center',
    fontSize: 20,
    marginTop: 10,
    marginBottom: 10
  }
});

