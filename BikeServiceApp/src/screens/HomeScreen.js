import React, { Component } from 'react';
import {View, Text, TouchableOpacity, StyleSheet, BackHandler} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import TasksController from '../controllers/TasksController'
import LoginScreen from '../screens/LoginScreen';
import UsersTasks from '../components/UsersTasks';
import User from '../objects/User';

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
      <View style={styles.conteiner}>
        <UsersTasks/>
        {this.state.loged === false ?
          <TouchableOpacity style={styles.searchButton} onPress={() => {
            BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
            this.props.navigation.push('Login');
          }}>
            <Text style={styles.buttonText}>LogIn</Text>
          </TouchableOpacity>
          :
          <TouchableOpacity style={styles.searchButton} onPress={() => {
            BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
            this.props.navigation.navigate('ControllPanel');
          }}>
            <Text style={styles.buttonText}>Admin</Text>
          </TouchableOpacity>
        }
      </View>
    );
  }
}

export default HomeScreen;

const styles = StyleSheet.create({
  searchButton: {
    alignSelf: 'flex-end',
    position: 'absolute',
    justifyContent: 'center',
    backgroundColor: '#249ef0',
    bottom: 10,
    right: 10,
    width: 60,
    height: 60,
    padding: 5,
    borderRadius: 60 / 2
  },
  buttonText : {
    color: 'white',
    textAlign: 'center'
  },
  conteiner:{
    flexDirection: 'column',
    flex: 1
  }
});




