import React, { Component } from 'react';
import {View, Text, TouchableOpacity, StyleSheet} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import LoginScreen from '../screens/LoginScreen';
import UsersTasks from '../components/UsersTasks';
import User from '../objects/User';

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
      <View style={styles.conteiner}>
        <UsersTasks/>
        {this.state.loged === false ?

          <TouchableOpacity style={styles.searchButton} onPress={() => this.props.navigation.navigate('Login')}>
            <Text style={styles.buttonText}>LogIn</Text>
          </TouchableOpacity>
          :
          <TouchableOpacity style={styles.searchButton} onPress={() => this.props.navigation.navigate('ControllPanel')}>
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




