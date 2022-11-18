import React, { Component } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, TextInput, BackHandler} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import User from '../objects/User'
import UserController from '../controllers/UserController';
import TasksController from '../controllers/TasksController';

class LoginScreen extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);

    this.state = {
      taskNumber: 0,
      showTask: false,
      password: '',
      login: ''
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

  async logIn(){
    var id = UserController.checkIsUser();
    if(id !== 0){
      await AsyncStorage.setItem('@BikeServiceUser', String(id))
      User.user.Id = id;
      User.user.Login = this.state.login;
      User.user.password = this.state.password;
      this.props.navigation.push('ControllPanel');
    }
  }

  render() {
    return (
      <View >
        <Text style={styles.text}>Logowanie</Text>
        <TextInput 
          style={styles.textInput}
          placeholder="login" 
          placeholderTextColor="gray" 
          onChangeText={text => this.setState({login: text})}
        />
         <TextInput 
          style={styles.textInput}
          secureTextEntry={true}
          placeholder="hasło" 
          placeholderTextColor="gray" 
          onChangeText={text => this.setState({password: text})}
        />
        <TouchableOpacity style={styles.searchButton} onPress={this.logIn.bind(this)}>
          <Text style={styles.buttonText}>Zaloguj</Text>
        </TouchableOpacity>
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
  },
  textInput :{
    color: '#000000',
    borderWidth: 1,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginBottom: 10,
    width: 200,
    height: 30,
    padding: 5,
    backgroundColor: 'white'
  },
  searchButton: {
    alignItems: 'center',
    marginLeft:'auto',
    marginRight:'auto',
    justifyContent: 'center',
    width: 100,
    padding: 5,
    backgroundColor: '#249ef0',
    borderRadius: 5,
    zIndex: 100
  },
  buttonText:{
    color: 'white'
  }
});

