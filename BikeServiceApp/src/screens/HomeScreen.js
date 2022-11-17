import React, { Component } from 'react';
import {View, Text, Button } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import LoginScreen from '../screens/LoginScreen';
import UsersTasksScreen from '../screens/UsersTasksScreen';
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
            onPress={() => this.props.navigation.navigate('Admin') }
            title="Panel administracyjny"
          />
        }
        <UsersTasksScreen/>
      </View>
    );
  }
}

export default HomeScreen;




