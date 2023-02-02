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
      error: {},
      orderHeader: 1,
      orderState: 1,
      orderInitDate: 1
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

  sortTask(property, order){
    TasksController.tasksList = TasksController.Sort(property, order)

    if(property == 'Header'){
      this.setState({orderHeader : this.state.orderHeader == 1 ? -1 : 1});
    }

    if(property == 'State'){
      this.setState({orderState : this.state.orderState == 1 ? -1 : 1});
    }

    if(property == 'InitDate'){
      this.setState({orderInitDate : this.state.orderInitDate == 1 ? -1 : 1});
    }

    this.setState({ refreshed: this.state.refreshed + 1 });
  }
  
  render() {
    return (
      <View style={mainStyle.conteiner}>
        <Button 
            onPress={this.logOut.bind(this)}
            title="Wyloguj"
        />
        <View style={ {display: 'flex', flexDirection: 'row', justifyContent: 'space-evenly'}}>
          <TouchableOpacity 
            style={ controlPanelStyle.searchBtn} 
            onPress={this.sortTask.bind(this, 'Header', this.state.orderHeader)}
          >
            <Text style={ controlPanelStyle.searchBtnText}>Sortuj stan</Text>
          </TouchableOpacity>
          <TouchableOpacity 
            style={ controlPanelStyle.searchBtn}
            onPress={this.sortTask.bind(this, 'State', this.state.orderState)}
          >
            <Text style={ controlPanelStyle.searchBtnText} >Sortuj stan</Text>
          </TouchableOpacity>
          <TouchableOpacity 
            style={ controlPanelStyle.searchBtn}
            onPress={this.sortTask.bind(this, 'InitDate', this.state.orderInitDate)}
          >
            <Text style={ controlPanelStyle.searchBtnText}>Sortuj date</Text>
          </TouchableOpacity>
        </View>
        <Text style={mainStyle.text}>Zadania</Text>
        <FlatList style={controlPanelStyle.flatList}
          data={TasksController.tasksList}
          extraData={this.state.refreshed}
          renderItem={({item}) => 
            <TouchableOpacity style={controlPanelStyle.listItem} onPress={this.editTask.bind(this, item)}>
              <Text style={controlPanelStyle.textList, {textAlign: 'center', marginTop: 5, width: '100%', marginLeft: 10, color: 'black', fontWeight: 'bold', fontSize: 16}}>{item.Header}</Text>
              <View style={ {width: '100%', paddingTop: 15, paddingBottom: 5, display: 'flex', flexDirection: 'row', justifyContent: 'center'}}>
                <Text style={controlPanelStyle.textList, {fontSize: 12, marginLeft: 10, color: 'black'}}>{item.InitDate}</Text>
                <Text style={controlPanelStyle.textList, {fontSize: 12, marginLeft: 'auto', marginRight: 10, color: 'black'}}>{String(Task.statusList.find(x => x.Value === item.State).Label)}</Text>
              </View>
             
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

