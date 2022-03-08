# 타워 디펜스 VR게임 제작
## 1. 프로젝트 개요
### 1.1 개발 인원/기간 및 포지션
- 1인, 총 5일 소요
- 프로그래밍 (VR환경 구축 및 개발)
### 1.2 개발 환경
- unity 2020.3.16f
- VR HMD : Oculus Quest 2
- 언어 : C#
- OS : Window 10
## 2. 개발 단계
- 에픽 게임즈 VR FPS < Bullet Train > 모티프 
- 프로토타입 버전: 기본 기능 구현 및 PC 버전
- 알파 타입 버전 : VR 컨트롤러 입력 대응 
## 3. 핵심 구현 내용 
### 3.1 이동 조작
- 기본적인 이동 및 점프
	- 전후좌우 이동은 CharacterController의 Move 함수 사용
	- 등가속도 운동 공식이 적용된 yVelocity 값에 중력을 더하고 수직 속도에 점프 크기를 넣어 Jump 기능 구현
	- TransformDirection() 함수를 사용하여 카메라가 바라보는 방향으로 사용자의 입력 방향을 바꿈(사용자가 바라보는 방향으로 이동 구현)
- 텔레포트 기능 
	- Ray가 맞은 지점이 Terrain일 경우 텔레포트 UI와 Line Renderer 활성화, SetPosition() 함수를 사용해 시작점과 끝점에 라인 그리기
	- 텔레포트 UI의 크기가 거리에 따라 보정되도록 original scale 설정, normal 벡터 방향을 forward로 해 UI가 하늘을 바라보도록 그림
	- Get/GetDown/GetUp 메서드를 사용하여 컨트롤러의 버튼 입력 시의 처리와 순서 재정리
	- PostProcessing에서 사용하는 프로파일에서 Motion Blur 효과를 얻어오고 Warp() 함수 구현에 적용, 시작점에서 도착점까지의 이동을 선형 보간(Lerp)으로 처리
### 3.2 무기
- 총 
	- 조준점 Crosshair 활성화, IndexTrigger 버튼 입력 시 Ray를 발사
	- 총알 파편 파티클 시스템 & 총알 발사 사운드 실행
## 4. 문제 해결 내용
### 4.1 플레이어가 타워에서 바닥으로 떨어질 때 순간 이동하는 듯이 부자연스럽게 떨어지는 현상 발생
- 수직 속도인 yVelocity가 타워 위에 있을 때 떨어지지 않는데도 계속 누적돼 실제 공중에 있을 때 그 값이 너무 커져서 발생한 문제
- 실제로 바닥에 있을 때는 반발력에 해당하는 수직 항력이 발생
- 바닥에 있는 경우 yVelocity를 0으로 초기화시켜 문제 해결
```
if(cc.isGrounded)
{
	yVelocity = 0;
}
```
