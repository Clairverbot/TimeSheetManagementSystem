import React, { Component } from 'react';
import { Card, Feed, Loader, CardContent } from 'semantic-ui-react';
import AuthContext from '../AuthContext'
import axios from 'axios';
import moment from 'moment';
import { LineChart, Line, CartesianGrid,XAxis,YAxis,Tooltip,Legend } from 'recharts';

export class Home extends Component {
  displayName = Home.name
  constructor(props) {
    super(props);
    this.state = {
      //notification
      notificationData: [],
      notificationLoading: true,
      //chart
      data: [],
      chartLoading: true
    }
    var config = {
      withCredentials: false,
      headers: {
        'Authorization': 'Bearer ' + this.props.token
      }
    }

    axios.get('/api/CustomerAccounts/GetCustAccByDate', config)
      .then(response => {
        this.setState({
          notificationData: response.data.sort(function (a, b) {
            return b.customerAccountId - a.customerAccountId;
          }),
          notificationLoading: false,
        });
      }
      );

    axios.get('/api/AccountRates/GetTotalEarnings', config)
      .then(response => {
        console.log(response.data);
        this.setState({
          data: response.data,
        });
        this.getChartData(response.data)
      }
      );
  }
  getChartData(data) {
    let temp = []
    for (var i = 0; i < Object.keys(data).length; i++) {
      temp.push({ name: Object.keys(data)[i].slice(0,-3), value: Object.values(data)[i] })
    }
    console.log(temp)
    this.setState({ data: temp, chartLoading: false })
    return temp
  }

  render() {
    return (
      <AuthContext.Consumer>
        {({ selectTask,role }) => (
          <div>
            <h1>Dashboard</h1>
            <Card fluid>
              <Card.Content>
                  <Card.Header>Total Monthly Rate per Hour</Card.Header>
                </Card.Content>
                {this.state.chartLoading ?
                  <Card.Content>
                    <Loader active inline='centered' />
                  </Card.Content>
                  :
                  <Card.Content>
                  <LineChart width={400} height={180} data={this.state.data}
                    margin={{ top: 0, right: 30, left: 0, bottom: 0 }}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Line type="monotone" dataKey="value" stroke="#52D4BF" />
                  </LineChart>
                  </Card.Content>
                }
              </Card>
              <Card fluid>
                <Card.Content>
                  <Card.Header>Recent Activities</Card.Header>
                </Card.Content>
                {this.state.notificationLoading ?
                  <Card.Content>
                    <Loader active inline='centered' />
                  </Card.Content>
                  :
                  this.state.notificationData.map((item) =>
                    <Card.Content>
                      <Feed>
                        <Feed.Event>
                          <Feed.Content>
                            <Feed.Date>
                            {/* {this.getDeDate(item.date)} */}
                            {moment.tz(item.date, "YYYYMMDD","Asia/Singapore").fromNow()}
                            </Feed.Date>
                            <Feed.Summary>
                              {item.createdBy} created customer account <a href='./CustomerAccount/Manage'>{item.accountName}</a>
                            </Feed.Summary>
                          </Feed.Content>
                        </Feed.Event>
                      </Feed>
                    </Card.Content>
                  )
                }
              </Card>
          </div>
        )}
      </AuthContext.Consumer>
    );
  }
}
