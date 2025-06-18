import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/UserService';
import { UserModel } from '../../models/UserModel';
import { AgChartsAngularModule } from 'ag-charts-angular';
import type {
  AgPolarChartOptions,
  AgPieSeriesOptions,
  AgCartesianChartOptions,
  AgBarSeriesOptions,
  AgLineSeriesOptions
} from 'ag-charts-community';
import { ChartConfiguration, ChartType, ChartDataset } from 'chart.js';
import { Navbar } from "../navbar/navbar";
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [AgChartsAngularModule, Navbar, RouterOutlet],
  templateUrl: './user-dashboard.html',
  styleUrls: ['./user-dashboard.css'],
})
export class UserDashboard implements OnInit {
  constructor(private userService: UserService) {}

  userData: UserModel[] = [];
  filteredUserData: UserModel[] = [];
  errorMessage = '';

  totalMales = 0;
  totalFemales = 0;

  // Chart.js configuration
  genderChartData: ChartConfiguration<'pie'>['data'] = {
    labels: ['Male', 'Female'],
    datasets: [
      {
        data: [0, 0],
        backgroundColor: ['#36A2EB', '#FF6384'],
        hoverOffset: 4,
      },
    ],
  };

  genderChartOptions: AgPolarChartOptions = {
    title: {
      text: 'Gender Distribution',
    },
    series: [
      {
        type: 'pie',
        angleKey: 'value',
        calloutLabelKey: 'label',
        data: [
          { label: 'Male', value: 0 },
          { label: 'Female', value: 0 },
        ],
      } as AgPieSeriesOptions,
    ],
  };

  genderChartType: ChartType = 'pie';

  // Donut chart for roles
  rolesChartOptions: AgPolarChartOptions = {
    title: {
      text: 'User Roles Distribution',
    },
    series: [
      {
        type: 'pie',
        angleKey: 'count',
        calloutLabelKey: 'role',
        innerRadiusRatio: 0.6, // This makes it a donut chart (0 = pie, 0.6 = 60% hole)
        data: [],
      } as AgPieSeriesOptions,
    ],
  };

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers() {
    this.userService.getAllUsers().subscribe({
      next: (data: any) => {
        this.userData = data.users.map((user: any) =>
          UserModel.mapUserData(user)
        );
        this.filteredUserData = [...this.userData];
        this.generateGenderChart();
        this.generateRolesChart();
        this.generateStateChart();
      },
      error: (err) => {
        this.errorMessage = `Error fetching user data`;
        console.error(this.errorMessage, err);
      },
    });
  }

  filterUsers(key: string, value: string) {
    this.userService.getFilteredUsers(key, value).subscribe({
      next: (data: any) => {
        this.filteredUserData = data.users.map((user: any) =>
          UserModel.mapUserData(user)
        );
      },
      error: (err) => {
        this.errorMessage = `Error filtering users by ${key}=${value}`;
        console.error(this.errorMessage, err);
      },
    });
  }

  generateGenderChart() {
    this.totalMales = 0;
    this.totalFemales = 0;

    this.userData.forEach((user) => {
      user.gender === 'male' ? this.totalMales++ : this.totalFemales++;
    });

    const total = this.userData.length;
    const malePercent = total
      ? +((this.totalMales * 100) / total).toFixed(2)
      : 0;
    const femalePercent = total
      ? +((this.totalFemales * 100) / total).toFixed(2)
      : 0;

    this.genderChartOptions = {
      ...this.genderChartOptions,
      series: [
        {
          type: 'pie',
          angleKey: 'value',
          calloutLabelKey: 'label',
          data: [
            { label: 'Male', value: malePercent },
            { label: 'Female', value: femalePercent },
          ],
          fills: ['#cefcfc', '#88eff9'],
          strokes: ['#ffffff', '#ffffff'],
          strokeWidth: 2,
          calloutLabel: {
            enabled: true,
            formatter: (params) =>
              `${params.datum.label}: ${params.datum.value}%`,
          },
          sectorLabel: {
            enabled: true,
            formatter: (params) => `${params.datum.value}%`,
          },
          highlightStyle: {
            item: {
              fill: undefined,
              stroke: '#07a6b4',
              strokeWidth: 2,
            },
          },
        } as AgPieSeriesOptions,
      ],
    };
  }

  generateRolesChart() {
    const roleCounts: { [key: string]: number } = {};

    this.userData.forEach((user) => {
      roleCounts[user.role] = (roleCounts[user.role] || 0) + 1;
    });

    const rolesData = Object.entries(roleCounts).map(([role, count]) => ({
      role,
      count,
    }));

    // Define a color palette for the roles
    const colors = [
      '#cefcfc',
      '#88eff9',
      '#46dfed'
    ];

    this.rolesChartOptions = {
      ...this.rolesChartOptions,
      series: [
        {
          type: 'pie',
          angleKey: 'count',
          calloutLabelKey: 'role',
          innerRadiusRatio: 0.6, // This creates the donut hole
          data: rolesData,
          fills: colors,
          strokes: ['#ffffff', '#ffffff', '#ffffff'],
          strokeWidth: 2,
          calloutLabel: {
            enabled: true,
            formatter: (params) =>
              `${params.datum.role}: ${params.datum.count}`,
          },
          sectorLabel: {
            enabled: true,
            formatter: (params) => `${params.datum.count}`,
            color: '#07a6b4',
            fontSize: 12,
          },
          highlightStyle: {
            item: {
              fill: undefined,
              stroke: '#07a6b4',
              strokeWidth: 2,
            },
          },
          tooltip: {
            renderer: (params) => {
              const total = this.userData.length;
              const percentage = ((params.datum.count / total) * 100).toFixed(
                2
              );
              return {
                title: params.datum.role,
                content: `${params.datum.count} (${percentage}%)`,
              };
            },
          },
        } as AgPieSeriesOptions,
      ],
    };
  }

  // Add this to your imports

// Add this property to your component class
stateChartOptions: AgCartesianChartOptions = {
  title: {
    text: 'User Distribution by State',
  },
  series: [
    {
      type: 'line',
      xKey: 'state',
      yKey: 'count',
      marker: {
        enabled: true,
        size: 10,
        fill: '#07a6b4',
        stroke: '#ffffff',
      },
      stroke: '#07a6b4',
      strokeWidth: 3,
      tooltip: {
        renderer: (params) => {
          return {
            title: params.datum.state,
            content: `${params.datum.count} users`,
          };
        },
      },
    } as AgLineSeriesOptions,
  ],
  axes: [
    {
      type: 'category',
      position: 'bottom',
      label: {
        rotation: -45,
        avoidCollisions: false,
      },
    },
    {
      type: 'number',
      position: 'left',
      title: {
        text: 'Number of Users',
      },
    },
  ],
};

generateStateChart() {
  const stateCounts: { [key: string]: number } = {};

  this.userData.forEach((user) => {
    const state = user.state;
    stateCounts[state] = (stateCounts[state] || 0) + 1;
  });

  const statesData = Object.entries(stateCounts)
    .map(([state, count]) => ({
      state,
      count,
    }))
    .sort((a, b) => b.count - a.count); // Sort by count descending

  this.stateChartOptions = {
    ...this.stateChartOptions,
    data: statesData,
  };
}

}
