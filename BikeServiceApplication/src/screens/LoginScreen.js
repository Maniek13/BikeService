import React, { Component } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, TextInput, BackHandler} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import Response from '../objects/Response';
import User from '../objects/User';
import UserController from '../controllers/UserController';
import Error from '../components/Error';

import Settings from '../objects/Settings';

import MainStyles from '../styles/MainStyles';
import LoginScreenStyles from '../styles/LoginScreenStyles'

class LoginScreen extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);

    this.state = {
      taskNumber: 0,
      btnLoginDisabled: false,
      password: '',
      login: '',
      error: {},
      showError: false
    };
  }

  componentDidMount (){
    this.focusListener = this.props.navigation.addListener('focus', () => {
      BackHandler.addEventListener('hardwareBackPress', this.handleBackButton);
    });
  }

  componentWillUnmount() {
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
  }

  handleBackButton(){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    this.props.navigation.push('Main');
    return true;
  } 

  onEndLoading(statment){
    if(statment === "if"){
      User.user.Id = Response.response.data.Id
      User.user.AppId = Response.response.data.AppId
      User.user.Login = this.state.login;
      User.user.Password = this.state.password;

      AsyncStorage.setItem('@BikeServiceUserId', String(User.user.Id))
      AsyncStorage.setItem('@BikeServiceUserLogin', String(this.state.login))
      AsyncStorage.setItem('@BikeServiceUserPassword', String(this.state.password))
      AsyncStorage.setItem('@BikeServiceUserAppId', String(User.user.AppId))

      BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
      this.props.navigation.push('ControllPanel');
    }

    if(statment === "else"){
      this.setState({
        error: Response.response
      });
      this.setState({ showError: true });
      this.setState({ btnLoginDisabled: false });
    }
  }

  async logIn(){
    this.setState({ showError: false });
    this.setState({ btnLoginDisabled: true});

    Response.response = {
      code: 0,
      data: {
        message: ''
      }
    }

    await UserController.checkIsUser(this.state.login, this.state.password);
    Response.getDate(this.onEndLoading.bind(this));
  }

  render() {
    return (
      <View>
        <Text style={mainStyle.text}>Logowanie</Text>
        <TextInput 
          style={loginScreenStyle.textInput}
          placeholder="login" 
          placeholderTextColor="gray" 
          onChangeText={text => this.setState({login: text})}
        />
         <TextInput 
          style={loginScreenStyle.textInput}
          secureTextEntry={true}
          placeholder="hasło" 
          placeholderTextColor="gray" 
          onChangeText={text => this.setState({password: text})}
        />
        <TouchableOpacity style={this.state.btnLoginDisabled ? mainStyle.buttonDisabled : mainStyle.buttonEnabled } onPress={this.logIn.bind(this)} disabled={this.state.btnLoginDisabled}>
          <Text style={mainStyle.buttonText}>Zaloguj</Text>
        </TouchableOpacity>
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
      </View>
    );
  }
}

export default LoginScreen;

const mainStyle = MainStyles[Settings.SchemaStyle]
const loginScreenStyle = LoginScreenStyles[Settings.SchemaStyle]

