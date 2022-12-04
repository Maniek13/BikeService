import React, { Component } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, TextInput, BackHandler} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import Response from '../objects/Response';
import User from '../objects/User';
import UserController from '../controllers/UserController';
import Error from '../components/Error';

class LoginScreen extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);

    this.state = {
      taskNumber: 0,
     // showTask: false,
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

  async logIn(){
    this.setState({ showError: false });
    await UserController.checkIsUser(this.state.login, this.state.password);

    var onTime = setInterval(() => {
      if(Response.response.code !== 0){
        if(Response.response.code === 1){
          User.user = Response.response.data;
          AsyncStorage.setItem('@BikeServiceUser', String(User.user.Id))
          BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
          this.props.navigation.push('ControllPanel');
        }
        else{
          this.setState({
            error: Response.response
          });
          this.setState({ showError: true });
        }
        
        Response.response = {
          code: 0,
          data: {
            message: ''
          }
        }

        clearInterval(onTime);
      }
    }, 100);
  }

  render() {
    return (
      <View>
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
        <TouchableOpacity style={styles.searchButton} onPress={this.logIn.bind(this)} disabled={false}>
          <Text style={styles.buttonText}>Zaloguj</Text>
        </TouchableOpacity>
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
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

