import React, { Component } from 'react';
import {View, Text, Button, BackHandler, StyleSheet, FlatList, TouchableOpacity} from 'react-native';

import AsyncStorage from '@react-native-async-storage/async-storage';

import TasksController from '../controllers/TasksController';
import User from '../objects/User';
import Task from '../objects/Task';
import Error from '../components/Error';
import Response from '../objects/Response';
import Settings from '../objects/Settings';

import MainStyles from '../styles/MainStyles';
import ControlPanelStyles from '../styles/ControlPanelStyles';

class ControllPanelScreen extends Component {
  constructor(props){
    super(props);
    this.handleBackButton = this.handleBackButton.bind(this);
    this.state = {
      refreshed: 0,
      showError: false,
      error: {}   
    };
  }

  onEndLoading(statment){
    if(statment === "if"){
      TasksController.tasksList = Response.response.data.Order;
      this.setState({ refreshed: this.state.refreshed + 1 });
    }

    if(statment === "else"){
      this.setState({
        error: Response.response
      });
      this.setState({ showError: true });
    }
  }

  async componentDidMount (){
    Response.response = {
      code: 0,
      data: {
        message: ''
      }
    }
    
    await TasksController.getTasks();
    this.setState({ showError: false });

    Response.getDate(this.onEndLoading.bind(this));

    this.focusListener = this.props.navigation.addListener('focus', () => {
      this.setState({ refreshed: this.state.refreshed + 1});
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

  async logOut(){
    await AsyncStorage.removeItem('@BikeServiceUserId');
    await AsyncStorage.removeItem('@BikeServiceUserLogin');
    await AsyncStorage.removeItem('@BikeServiceUserPassword');
    await AsyncStorage.removeItem('@BikeServiceUserAppId');

    User.user.Id = 0;
    User.user.Name = '';
    User.user.Password = '';
    TasksController.tasksList = [];

    this.handleBackButton()
  }

  editTask(item){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    Task.task = item;
    this.props.navigation.navigate('EditTask')
  }

  addTask(){
    BackHandler.removeEventListener('hardwareBackPress', this.handleBackButton);
    this.props.navigation.navigate('AddTask')
  }
  
  render() {
    return (
      <View style={mainStyle.conteiner}>
        <Button 
          onPress={this.logOut.bind(this)}
          title="Wyloguj"
        />
        <Text style={mainStyle.text}>Zadania</Text>
        <FlatList style={controlPanelStyle.flatList}
          data={TasksController.tasksList}
          extraData={this.state.refreshed}
          renderItem={({item}) => 
            <TouchableOpacity style={controlPanelStyle.listItem} onPress={this.editTask.bind(this, item)}>
              <Text style={controlPanelStyle.textList, {marginLeft: 10, color: 'black'}}>{item.Header}</Text>
              <Text style={controlPanelStyle.textList, {color: 'black'}}>{item.InitDate}</Text>
              <Text style={controlPanelStyle.textList, {marginLeft: 'auto', marginRight: 10, color: 'black'}}>{String(Task.statusList.find(x => x.Value === item.State).Label)}</Text>
            </TouchableOpacity>
          }
        />
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}

        <TouchableOpacity style={mainStyle.circleBtn} onPress={this.addTask.bind(this)}>
            <Text style={controlPanelStyle.buttonText}>+</Text>
        </TouchableOpacity>
      </View>
    );
  }
}

export default ControllPanelScreen;

const mainStyle = MainStyles[Settings.SchemaStyle]
const controlPanelStyle = ControlPanelStyles[Settings.SchemaStyle]

