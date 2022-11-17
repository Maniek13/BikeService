import React, { Component } from 'react';
import {View, Text, Button } from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import LoginScreen from '../screens/LoginScreen';
import UsersTasks from '../components/UsersTasks';
import User from '../objects/User'

class HomeScreen extends Component {
  constructor(props){
    super(props);

    this.state = {
      loged: false
    };
  }
 
  componentDidMount(){
    this.focusListener = this.props.navigation.addListener('focus', () => {
      this.chekIsLoged();
    });
  }

  async chekIsLoged(){
    var value = await AsyncStorage.getItem('@BikeServiceUser')

    if(value !== null){
      this.setState({ loged: true });
      User.user.Id = value;
    }
    else{
      this.setState({ loged: false });
    }
  }
  
  render() {
    return (
      <View>
        {this.state.loged === false ?
          <Button
            onPress={() => this.props.navigation.navigate('Login')  }
            title="Zaloguj"
          /> 
          :
          <Button
            onPress={() => this.props.navigation.navigate('ControllPanel') }
            title="Panel administracyjny"
          />
        }
        <UsersTasks/>
      </View>
    );
  }
}

export default HomeScreen;




