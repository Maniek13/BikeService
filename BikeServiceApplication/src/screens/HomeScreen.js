import React, { Component } from 'react';
import {View, Text, TouchableOpacity, StyleSheet, BackHandler} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import UsersTasks from '../components/UsersTasks';
import User from '../objects/User';

import mainStyle from '../styles/MainStyle';

class HomeScreen extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);

    this.state = {
      loged: false
    };
  }
 
  componentDidMount(){
    this.focusListener = this.props.navigation.addListener('focus', () => {
      this.chekIsLoged();
      BackHandler.addEventListener('hardwareBackPress', this.handleBackButton);
    });
  }
  
  handleBackButton(){
    BackHandler.exitApp()
    return true;
  } 
  
  componentWillUnmount() {
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
  }

  async chekIsLoged(){
    let value = await AsyncStorage.getItem('@BikeServiceUserId')

    if(value !== null){
      this.setState({ loged: true });

      User.user.Login = await AsyncStorage.getItem('@BikeServiceUserLogin')
      User.user.Password = await AsyncStorage.getItem('@BikeServiceUserPassword')
      User.user.AppId = await AsyncStorage.getItem('@BikeServiceUserAppId')

      User.user.Id = value;
    }
    else{
      this.setState({ loged: false });
    }
  }
  render() {
    return (
      <View style={mainStyle.conteiner}>
        <UsersTasks/>
        {this.state.loged === false ?
          <TouchableOpacity 
            style={mainStyle.circleBtn} onPress={() => {
            BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
            this.props.navigation.push('Login');
          }}>
            <Text style={mainStyle.buttonText}>LogIn</Text>
          </TouchableOpacity>
          :
          <TouchableOpacity          
            style={mainStyle.circleBtn} onPress={() => {
            BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
            this.props.navigation.navigate('ControllPanel');
          }}>
            <Text style={mainStyle.buttonText}>Admin</Text>
          </TouchableOpacity>
        }
      </View>
    );
  }
}

export default HomeScreen;


