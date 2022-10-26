const students = [
    { name: 'Alex', grade: 15, point: 15 },
    { name: 'Devlin', grade: 15, point: 25 },    
    { name: 'Eagle', grade: 14, point: 12 },
    { name: 'Sam', grade: 13, point: 26 }
];

// Sort by Name (asc, desc)
const studentsSortByNameAscending = students.slice().sort((a, b) => {
    if (a.name < b.name) return -1;
    if (a.name > b.name) return 1;

    return 0;
});
const studentsSortByNameDescending = students.slice().sort((a, b) => {
    if (a.name < b.name) return 1;
    if (a.name > b.name) return -1;

    return 0;
});

console.log('Sort by Name (asc, desc)');
console.log(studentsSortByNameAscending);
console.log(studentsSortByNameDescending);

// Sort by Grade (asc, desc)
const studentsSortByGradeAscending = students.slice().sort((a, b) => b.grade - a.grade);
const studentsSortByGradeDescending = students.slice().sort((a, b) => a.grade - b.grade);

console.log('Sort by Grade (asc, desc)')
console.log(studentsSortByGradeAscending);
console.log(studentsSortByGradeDescending);

// Filter students have point greater than 15
const studentsHavePointGreaterThan15 = students.filter(s => s.point > 15 );

console.log('Filter students have point greater than 15');
console.log(studentsHavePointGreaterThan15);

// Sum point of students have grade equal to 15
const totalPointOfStudentGradeIs15 = students
    .filter(s => s.grade === 15)
    .reduce((prev, curr) => prev + curr.point, 0);

console.log('Sum point of students have grade equal to 15');
console.log(totalPointOfStudentGradeIs15);

// Log all students not named Sam
const studentsWithoutSam = students.filter(s => s.name != 'Sam');

console.log('Log all students not named Sam');
console.log(studentsWithoutSam);

